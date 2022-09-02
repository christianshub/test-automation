#include "Utility.h"

std::string GetDesktopPath() {

    CHAR path[MAX_PATH];
    HRESULT hRes = SHGetFolderPathA(NULL, CSIDL_DESKTOP, 0, NULL, path);

    if (SUCCEEDED(hRes))
    {
        return (std::string(path) + "\\");
    }
    return "Couldn't retrieve desktop path\n";
}


std::string CreateFolder(std::string path, std::string folderName)
{
    path += folderName + '\\';

    CreateDirectoryA(path.c_str(), NULL);

    return path;
}

timeStruct TimeToString()
{
    std::chrono::system_clock::time_point p = std::chrono::system_clock::now();
    time_t t = std::chrono::system_clock::to_time_t(p);
    char str[26];
    ctime_s(str, sizeof str, &t);

    std::vector<std::string> result;
    std::istringstream ss(str);
    for (std::string s; ss >> s; )
        result.push_back(s);

    timeStruct myStruct;
    myStruct.dayname = result[0];
    myStruct.month = result[1];
    myStruct.day = result[2];
    myStruct.time = result[3];
    myStruct.year = result[4];

    return myStruct;
}


void Stealth()
{
    HWND Stealth;
    AllocConsole();
    Stealth = FindWindowA("ConsoleWindowClass", NULL);
    ShowWindow(Stealth, 0);
}


bool FindSubStr(std::string& mainStr, const std::string toFind)
{
    std::size_t found = mainStr.find(toFind);

    if (found != std::string::npos)
        return true;

    return false;
}