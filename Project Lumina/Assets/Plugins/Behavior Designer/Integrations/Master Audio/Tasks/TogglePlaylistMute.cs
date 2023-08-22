using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Playlist")]
    [TaskDescription("Toggle mute on a Playlist.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class TooglePlaylistMute : Action
    {
        [Tooltip("Check this to perform action on all Playlist Controllers")]
        public SharedBool allPlaylistControllers;
        [Tooltip("Name of Playlist Controller containing the Playlist. Not required if you only have one Playlist Controller")]
        public SharedString playlistControllerName;

        public override TaskStatus OnUpdate()
        {
            if (allPlaylistControllers.Value) {
                var pcs = PlaylistController.Instances;

                for (var i = 0; i < pcs.Count; i++) {
                    MasterAudio.ToggleMuteAllPlaylists();
                }
            } else {
                if (string.IsNullOrEmpty(playlistControllerName.Value)) {
                    MasterAudio.ToggleMutePlaylist();
                } else {
                    MasterAudio.ToggleMutePlaylist(playlistControllerName.Value);
                }
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            allPlaylistControllers = false;
            playlistControllerName = "";
        }
    }
}