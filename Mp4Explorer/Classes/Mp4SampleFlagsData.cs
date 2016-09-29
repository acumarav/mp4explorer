//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class Mp4SampleFlagsData
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private uint flags;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        public Mp4SampleFlagsData(uint flags)
        {
            this.flags = flags;
        }

        /// <summary>
        /// 
        /// </summary>
        public uint Reserverd
        {
            get
            {
                return flags >> 26 & 0x3F;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool SampleDependsOn
        {
            get
            {
                return (flags >> 24 & 0x03) != 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool SampleIsDependOn
        {
            get
            {
                return (flags >> 22 & 0x03) != 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool SampleHasRedudancy
        {
            get
            {
                return (flags >> 20 & 0x03) != 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte SamplePaddingValue
        {
            get
            {
                return (byte)(flags >> 17 & 0x07);
            }
        }

        /// <summary>
        /// When 1 signals a non-key or non-sync sample.
        /// </summary>
        public bool SampleIsDifferenceSample
        {
            get
            {
                return (flags >> 16 & 0x01) != 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public uint SampleDegradationPriority
        {
            get
            {
                return (byte)(flags >> 17 & 0xFFFF);
            }
        }
    }
}
