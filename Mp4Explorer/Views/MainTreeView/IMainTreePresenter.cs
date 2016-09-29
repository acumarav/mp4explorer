//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.IO;

namespace Mp4Explorer
{
    public interface IMainTreePresenter
    {
        IMainTreeView View { get; set; }

        void OpenFile(FileStream file);
    }
}
