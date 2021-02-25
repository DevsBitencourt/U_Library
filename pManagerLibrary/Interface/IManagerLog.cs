using pManagerLibrary.Enum;

namespace pManagerLibrary.Interface
{
    interface IManagerLog
    {
        void Recording(string MsgLog, TManagerEnum model);
        string[] Reading(TManagerEnum model);
        string GetSequenceFile(TManagerEnum model);
        void OpenFolder(string pathFile, TManagerEnum model);
    }
}
