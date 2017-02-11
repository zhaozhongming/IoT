using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    [
    DebuggerDisplay("{BpcUid}:{WinUid}-{TimeStamp}"),
    DataContract
    ]
    public class InputDTO
    //: IEnumerable<float>
    {
        public InputDTO()
        {
            this.__values = new float[16];
        }

        /// <summary>Board PC unique identification number.</summary>
        ///
        [
        DataMember(Order = 0, Name = @"bpc")
        ]
        public int BpcUid
        {
            get { return this._BpcUid; }
            set { _BpcUid = value; }
        }
        private int _BpcUid;

        /// <summary>WIN unique identification number.</summary>
        ///
        [
        DataMember(Order = 1, Name = @"win")
        ]
        public int WinUid { get; set; }

        /// <summary>WIN unique identification number.</summary>
        ///
        [
        DataMember(Order = 2, Name = @"time")
        ]
        public DateTime TimeStamp { get; set; }

        /// <summary>Input value from channel #01.</summary>
        ///
        [IgnoreDataMember]
        public float Ch01 { get { return this._values[0]; } set { this._values[0] = value; } }

        /// <summary>Input value from channel #02.</summary>
        ///
        [IgnoreDataMember]
        public float Ch02 { get { return this._values[1]; } set { this._values[1] = value; } }

        /// <summary>Input value from channel #03.</summary>
        ///
        [IgnoreDataMember]
        public float Ch03 { get { return this._values[2]; } set { this._values[2] = value; } }

        /// <summary>Input value from channel #04.</summary>
        ///
        [IgnoreDataMember]
        public float Ch04 { get { return this._values[3]; } set { this._values[3] = value; } }

        /// <summary>Input value from channel #05.</summary>
        ///
        [IgnoreDataMember]
        public float Ch05 { get { return this._values[4]; } set { this._values[4] = value; } }

        /// <summary>Input value from channel #06.</summary>
        ///
        [IgnoreDataMember]
        public float Ch06 { get { return this._values[5]; } set { this._values[5] = value; } }

        /// <summary>Input value from channel #07.</summary>
        ///
        [IgnoreDataMember]
        public float Ch07 { get { return this._values[6]; } set { this._values[6] = value; } }

        /// <summary>Input value from channel #08.</summary>
        ///
        [IgnoreDataMember]
        public float Ch08 { get { return this._values[7]; } set { this._values[7] = value; } }

        /// <summary>Input value from channel #09.</summary>
        ///
        [IgnoreDataMember]
        public float Ch09 { get { return this._values[8]; } set { this._values[8] = value; } }

        /// <summary>Input value from channel #10.</summary>
        ///
        [IgnoreDataMember]
        public float Ch10 { get { return this._values[9]; } set { this._values[9] = value; } }

        /// <summary>Input value from channel #11.</summary>
        ///
        [IgnoreDataMember]
        public float Ch11 { get { return this._values[10]; } set { this._values[10] = value; } }

        /// <summary>Input value from channel #12.</summary>
        ///
        [IgnoreDataMember]
        public float Ch12 { get { return this._values[11]; } set { this._values[11] = value; } }

        /// <summary>Input value from channel #13.</summary>
        ///
        [IgnoreDataMember]
        public float Ch13 { get { return this._values[12]; } set { this._values[12] = value; } }

        /// <summary>Input value from channel #14.</summary>
        ///
        [IgnoreDataMember]
        public float Ch14 { get { return this._values[13]; } set { this._values[13] = value; } }

        /// <summary>Input value from channel #15.</summary>
        ///
        [IgnoreDataMember]
        public float Ch15 { get { return this._values[14]; } set { this._values[14] = value; } }

        /// <summary>Input value from channel #16.</summary>
        ///
        [IgnoreDataMember]
        public float Ch16 { get { return this._values[15]; } set { this._values[15] = value; } }

        [IgnoreDataMember]
        private const int _CountValue = 16;

        public int Count { get { return _CountValue; } }

        public float this[int index]
        {
            get { return this._values[index]; }
            set { this._values[index] = value; }
        }

        private float[] __values;

        [
        DataMember(Order = 20, Name = @"data"),
        DebuggerBrowsable(DebuggerBrowsableState.Never)
        ]
        private float[] _values
        {
            get
            {
                if (__values == null)
                    __values = new float[16];
                return __values;
            }
            set
            {
                __values = value;
            }
        }


        //public IEnumerator<float> GetEnumerator()
        //{
        //    foreach (var v in this._values)
        //        yield return v;
        //}

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return this.GetEnumerator();
        //}
    }
}
