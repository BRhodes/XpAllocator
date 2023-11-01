using System;

using Decal.Adapter;
using Decal.Adapter.Wrappers;

namespace XpAllocator
{
	internal static class Globals
	{
		public static void Init(string pluginName, CoreManager core)
		{
			PluginName = pluginName;

			Core = core;
		}

		public static string PluginName { get; private set; }

		public static CoreManager Core { get; private set; }

        static public PlayerConfiguration Config { get; set; }
        static public XpAllocator XpAllocator { get; set; }
    }
}
