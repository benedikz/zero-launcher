using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ZeroApp.DiscordRichPresence;

namespace ZeroApp
{
    class RPCManager
    {
        private static DiscordRichPresence.RichPresence richPresence;
                static DiscordRichPresence.EventHandlers eventHandlers;

        // Rich Presence Control Methods

        public static void Initialize(string clientId)
        {
            eventHandlers = new DiscordRichPresence.EventHandlers();
            eventHandlers.readyCallback = Callback_Ready;
            eventHandlers.disconnectedCallback += Callback_Disconnected;
            eventHandlers.errorCallback += Callback_Error;

            DiscordRichPresence.Initialize(clientId, ref eventHandlers, true, null);
            Trace.WriteLine("[RPC] Rich Presence Initialized.");
        }

        public static void UpdatePresence(string details, string state, long timestamp_start, long timestamp_end, string largeImageKey, string largeImageText, string smallImageKey, string smallImageText)
        {
            richPresence.details = details;
            richPresence.state = state;
            richPresence.startTimestamp = timestamp_start;
            richPresence.endTimestamp = timestamp_end;
            richPresence.largeImageKey = largeImageKey;
            richPresence.largeImageText = largeImageText;
            richPresence.smallImageKey = smallImageKey;
            richPresence.smallImageText = smallImageText;

            DiscordRichPresence.UpdatePresence(ref richPresence);
            Trace.WriteLine("[RPC] Rich Presence Updated.");
        }

        public static void ShutdownPresence()
        {
            DiscordRichPresence.Shutdown();
            Trace.WriteLine("[RPC] Rich Presence Shutdown.");
        }

        // Event Handlers for Remote Procedure Calls

        private static void Callback_Ready()
        {
            Trace.WriteLine("[RPC] Callback Ready.");
        }

        private static void Callback_Disconnected(int error_code, string message)
        {
            // Přidat error_code a message
            Trace.WriteLine("[RPC] Callback Disconnected.");
        }

        private static void Callback_Error(int error_code, string message)
        {
            // Přidat error_code a message
            Trace.WriteLine("[RPC] Callback Error.");
        }
    }

    class DiscordRichPresence
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReadyCallback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DisconnectedCallback(int errorCode, string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ErrorCallback(int errorCode, string message);

        public struct EventHandlers
        {
            public ReadyCallback readyCallback;
            public DisconnectedCallback disconnectedCallback;
            public ErrorCallback errorCallback;
        }

        // Values explanation and example: https://discordapp.com/developers/docs/rich-presence/how-to#updating-presence-update-presence-payload-fields
        [System.Serializable]
        public struct RichPresence
        {
            public string state; /* max 128 bytes */
            public string details; /* max 128 bytes */
            public long startTimestamp;
            public long endTimestamp;
            public string largeImageKey; /* max 32 bytes */
            public string largeImageText; /* max 128 bytes */
            public string smallImageKey; /* max 32 bytes */
            public string smallImageText; /* max 128 bytes */
            public string partyId; /* max 128 bytes */
            public int partySize;
            public int partyMax;
            public string matchSecret; /* max 128 bytes */
            public string joinSecret; /* max 128 bytes */
            public string spectateSecret; /* max 128 bytes */
            public bool instance;
        }

        [DllImport(App.DLL, EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);

        [DllImport(App.DLL, EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdatePresence(ref RichPresence presence);

        [DllImport(App.DLL, EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunCallbacks();

        [DllImport(App.DLL, EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown();
    }
}
