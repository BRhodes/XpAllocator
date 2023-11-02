using Decal.Adapter;
using System;
using System.IO;

namespace XpAllocator
{
	public static class Util
	{
        public static void WriteToChat(string message)
        {
            CoreManager.Current.Actions.AddChatText("<{" + Globals.PluginName + "}>: " + message, 3);
        }
    }
}
