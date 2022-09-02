#include "Utility.h"
#include <iostream>
int main()
{
    SetConsoleTitle(L"CountPresses");

    std::string desktopPath = GetDesktopPath();
    std::string path = CreateFolder(desktopPath, "Count");

    timeStruct datetime = TimeToString();
    
    std::string filename = path + datetime.year + "_" + datetime.month + "_" + datetime.day + ".txt";

    Stealth();

    int keyCount = 0;
    int mouseCount = 0;
    
    while (true)
    {
        timeStruct getTime = TimeToString();
        if ( (FindSubStr(getTime.time, ":00:") == 1) || (FindSubStr(getTime.time, ":15:") == 1) || (FindSubStr(getTime.time, ":30:") == 1)  || (FindSubStr(getTime.time, ":45:") == 1) )
        {
            std::ofstream myfile;
            myfile.open(filename);

            myfile << "start time:   " << datetime.time << std::endl;
            myfile << "last updated: " << getTime.time << std::endl;
            myfile << "keyCount:     " << keyCount << std::endl;
            myfile << "mouseCount:   " << mouseCount << std::endl;
            
            myfile.close();
            Sleep(60000);
        }

        for (int i = 3; i < 256; i++)
        {
			if (GetAsyncKeyState(i) & 1) 
			{
                keyCount++;
			}
        }

        for (int i = 0; i < 3; i++)
        {
            if (GetAsyncKeyState(i) & 1)
            {
                mouseCount++;
            }
        }
        Sleep(500);
    }
    
}
