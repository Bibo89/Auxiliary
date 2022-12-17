# 📚 Auxiliary

Auxiliary is a tool for Terraria servers to make database interaction, configuration management, packet handling, events & logging much easier for plugin developers.

## ⚙️ Configuration

Auxiliary has a static handler for configuration that integrates into the default tshock design without added effort. It is incredibly straight forward to implement.

### 🏗️ Building your configurations

#### Defining the settings

```cs
public class PluginSettings : ISettings
{
  [JsonPropertyName("mysettingvalue")]
  public string Value { get; set; }
}
```

You can populate as many values as you wish, and the following step will automatically handle the creation and reading of the file.

#### Reading the settings

```cs
public override void Initialize()
{
  Configuration<PluginSettings>.Load(nameof(Plugin));

  GeneralHooks.ReloadEvent += (x) =>
  {
    Configuration<PluginSettings>.Load(nameof(Plugin));
    x.Player.SendSuccessMessage("[Plugin] Reloaded configuration.");
  };
}
```

By adding the following code in your initialize method, it will automatically load the configuration for use, and the ability to reload configuration at runtime. Call the `Loaded` property to ensure the configuration is loaded if you plan to access it in events you are not sure run before initialize or not.

#### Using the settings

```cs
var value = Configuration<PluginSettings>.Settings.Value;
```

### ♻️ Auxiliaries own configuration

Auxiliary has its own auto generated configuration file it needs to connect to the database and define a path for both mongodb and the local file system to write to.

```json
{
  "connectionstring": "",
  "mongopath": "",
  "localpath": ""
}
```

- `connectionstring` is the value used to connect to the database. This has to be a `mongodb://` prefixed string.
- `mongopath` represents the database name used after connecting to the database.
- `localpath` is the local **relative** path to the execution environment Auxiliary is allowed to use to read and write json.

## 📑 Data Models

### 🗃️ Json

Json integrates in a file pattern, keeping a file reference and reading/writing to it actively during model modification. These models can be used for smaller data storage, for example for keeping data for specific actions.

#### A Json model:

```cs
public class PluginValue : JsonModel
{
  private int _value;
  public int Value 
  {
    get
      => _value;
    set
    {
      _value = value;
      _ = this.SaveAsync();
    }
}
```

By allowing the getter to save the model after modifying its underlying field, it will be able to dynamically save on all edits, eliminating the need for self written `SaveAsync` implementations. This design should not persist on models that are concurrently operated on, as it will cause concurrency errors. If models are frequently modified, the Bson design should be preferred.

#### Fetching our model:

```cs
var guaranteedModel = await IModel.GetAsync(GetRequest.Json<PluginValue>(x => x.Value == 1), x => x.Value = 1);

var nullIfNotFound = await IModel.GetAsync(GetRequest.Json<PluginValue>(x => x.Value == 1));
```

The `guaranteedModel` writes to a property called `creationAction`. If this property is populated at the moment you make a get request, it will automatically create a new `PluginValue` with the provided values, push it into the local file, and return it.

The `nullIfNotFound` keeps the `creationAction` empty. If Auxiliary does not see a creation action present, it will ignore creation patterns and return `null` if the model cannot be found in the local file.

#### Creating a model:

If you prefer to create a model without the getter design, it is possible to do so through the `CreateAsync` member.

```cs
var createdModel = await IModel.CreateAsync(CreateRequest.Json<PluginValue>(x => x.Value = 2));
```

### 🗄️ Bson

MongoDB integrates seamlessly into Auxiliary, offering a mutual design to json models. Only this time, we use a different reference to fetch and create models.

#### A Bson model:

```cs
public class PluginUser : BsonModel 
{

  private string _name = string.Empty;
  public string Name
  {
    get
      => _name;   
    set
    {
      _ = this.SaveAsync(x => x.Name, value);
      _name = value;
    }
  }
}
```

Every time the setter of this object is called, it makes an atomic call to the database and updates the value in the database model.
Because of this, data can be concurrently handled and run effortlessly in async context. All state handling is done in the background.

#### Fetching our model:

```cs
var guaranteedModel = await IModel.GetAsync(GetRequest.Bson<PluginUser>(x => x.Name == "name"), x => x.Name = "name");

var nullIfNotFound = await IModel.GetAsync(GetRequest.Bson<PluginUser>(x => x.Name == "name"));
```

#### Creating new models:

```cs
var createdModel = await IModel.CreateAsync(CreateRequest.Bson<PluginUser>(x => x.Name = "silly goose"));
