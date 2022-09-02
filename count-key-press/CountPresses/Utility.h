#pragma once

#include <iostream>
#include <fstream>
#include <shlobj.h> // SHGetFolderPathA, CSIDL_DESKTOP
#include <chrono>
#include <vector>
#include <sstream>


struct timeStruct {
    std::string dayname = "";
    std::string month = "";
    std::string day = "";
    std::string time = "";
    std::string year = "";
};

timeStruct TimeToString();

std::string GetDesktopPath();

std::string CreateFolder(std::string path, std::string folderName);

void Stealth();

bool FindSubStr(std::string& mainStr, const std::string toFind);
