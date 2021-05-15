#ifndef UNICODE
#define UNICODE
#endif 

#include <windows.h>
#include <algorithm>
#include <stdio.h>
#include <string.h>
#include <vector>

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM lp);
void outputToConsole(const wchar_t* text);

std::vector<HWND> hwnds;

BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM lp)
{
    WCHAR windowTitle[256] = { };
    auto size = sizeof(windowTitle) / sizeof(windowTitle[0]);
    GetWindowText(hwnd,  windowTitle, size);
    if (windowTitle[0] == 0) return TRUE;

    auto findP = wcsstr(windowTitle, L"Chrome");
    if (findP != nullptr) {
        outputToConsole(windowTitle);
        wchar_t format[64] = {};
        wsprintf(format, L"%p", hwnd);
        outputToConsole(format);
        hwnds.push_back(hwnd);

        SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
    }
    else {
        outputToConsole(L"not found ");
    }
    return true;
}

void outputToConsole(const wchar_t* text)
{
    HANDLE hStdOutput = GetStdHandle(STD_OUTPUT_HANDLE);

    DWORD dwWriteByte;
    TCHAR szBuf[256];
    lstrcpy(szBuf, text);
    WriteConsole(hStdOutput, szBuf, lstrlen(szBuf), &dwWriteByte, NULL);

    TCHAR newline[256];
    lstrcpy(newline, L"\n");
    WriteConsole(hStdOutput, newline, lstrlen(newline), &dwWriteByte, NULL);
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PWSTR pCmdLine, int nCmdShow)
{
    // Register the window class.
    const wchar_t CLASS_NAME[] = L"Sample Window Class";

    WNDCLASS wc = { };

    wc.lpfnWndProc = WindowProc;
    wc.hInstance = hInstance;
    wc.lpszClassName = CLASS_NAME;

    RegisterClass(&wc);

    AllocConsole();
    AllowSetForegroundWindow(ASFW_ANY);

    // Create the window.
    HWND hwnd = CreateWindowEx(
        WS_EX_TOPMOST | WS_EX_NOACTIVATE,            // Window style
        CLASS_NAME,                     // Window class
        L"Learn to Program Windows",    // Window text
        0,                              // Optional window styles.

        // Size and position
        CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,

        NULL,       // Parent window    
        NULL,       // Menu
        hInstance,  // Instance handle
        NULL        // Additional application data
    );

    BOOL res;
    NOTIFYICONDATA notifyIconData;
    notifyIconData.cbSize = sizeof(NOTIFYICONDATA);
    notifyIconData.hWnd = hwnd;
    notifyIconData.uID = 1001;
    notifyIconData.uFlags = NIF_MESSAGE | NIF_ICON | NIF_TIP;
    notifyIconData.uCallbackMessage = nullptr;
    HICON hicon;
    notifyIconData.hIcon = hicon;

    if (hwnd == NULL)
    {
        return 0;
    }

    ShowWindow(hwnd, SW_SHOWNA);

    // Run the message loop.

    MSG msg = { };
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return 0;
}

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
    case WM_DESTROY:
        FreeConsole();
        PostQuitMessage(0);
        return 0;

    case WM_PAINT:
    {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hwnd, &ps);

        FillRect(hdc, &ps.rcPaint, (HBRUSH)(COLOR_WINDOW + 1));

        EndPaint(hwnd, &ps);
        return 0;
    }
    case WM_CREATE:
    {
        EnumWindows(EnumWindowsProc, lParam);
        return 0;
    }
    }
    return DefWindowProc(hwnd, uMsg, wParam, lParam);
}