using Ktisis.Structs.Input;

using System;
using System.Runtime.InteropServices;

namespace Ktisis.Structs.GameWindow {

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct Window {
		public IntPtr Unk1;
		public fixed char WindowName[16];
		public fixed byte Unk2[368];
		public byte ClickedState;

		public bool IsClicked() {
			return ClickedState == 0x3C;
		}

		public IntPtr Self() {
			fixed (Window* ptr = &this)
				return (IntPtr)ptr;
		}

		public string Name() {
			fixed (char* ptr = WindowName)
				return new string(ptr);
		}
	}
}

