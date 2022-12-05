using Dalamud.Hooking;
using Dalamud.Logging;

using Ktisis.Structs.GameWindow;
using Ktisis.Structs.Input;

using System;
using static Ktisis.Interop.Hooks.ControlHooks;

namespace Ktisis.Interop.Hooks {
	internal static class WindowHooks {

		//a1: Base window stack ptr
		//a2: Current selected window ptr
		internal unsafe delegate void WindowDelegate(IntPtr a1, IntPtr a2);
		internal static Hook<WindowDelegate> WindowHook = null!;

		internal unsafe static void WindowDetour(IntPtr a1, IntPtr a2) {
			WindowHook.Original(a1, a2);
			PluginLog.Information($"a1=0x{a1:X}, a2=0x{a2:X}");
			//PluginLog.Information($"Clicked on {currentWindow->WindowName[0]}");
		}


		internal static void Init() {
			unsafe {
				var addr = Services.SigScanner.ScanText("E8 ?? ?? ?? ?? 80 8B 80 9C 00 00 01 B0 01 48 8B 7C 24 30");
				WindowHook = Hook<WindowDelegate>.FromAddress(addr, WindowDetour);
				WindowHook.Enable();

			}
		}

		internal static void Dispose() {
			WindowHook.Disable();
			WindowHook.Dispose();
		}
	}
}
