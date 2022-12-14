using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary
{
    /// <summary>
    ///     Represents how this model is stored.
    /// </summary>
    public enum ModelType : byte
    {
        /// <summary>
        ///     Represents a JSON model.
        /// </summary>
        Json,

        /// <summary>
        ///     Represents a BSON model.
        /// </summary>
        Bson
    }
}
