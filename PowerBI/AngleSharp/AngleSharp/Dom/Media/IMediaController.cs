using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001CF RID: 463
	[DomName("MediaController")]
	public interface IMediaController
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F60 RID: 3936
		[DomName("buffered")]
		ITimeRanges BufferedTime { get; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F61 RID: 3937
		[DomName("seekable")]
		ITimeRanges SeekableTime { get; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F62 RID: 3938
		[DomName("played")]
		ITimeRanges PlayedTime { get; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F63 RID: 3939
		[DomName("duration")]
		double Duration { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F64 RID: 3940
		// (set) Token: 0x06000F65 RID: 3941
		[DomName("currentTime")]
		double CurrentTime { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F66 RID: 3942
		// (set) Token: 0x06000F67 RID: 3943
		[DomName("defaultPlaybackRate")]
		double DefaultPlaybackRate { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F68 RID: 3944
		// (set) Token: 0x06000F69 RID: 3945
		[DomName("playbackRate")]
		double PlaybackRate { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F6A RID: 3946
		// (set) Token: 0x06000F6B RID: 3947
		[DomName("volume")]
		double Volume { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F6C RID: 3948
		// (set) Token: 0x06000F6D RID: 3949
		[DomName("muted")]
		bool IsMuted { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F6E RID: 3950
		[DomName("paused")]
		bool IsPaused { get; }

		// Token: 0x06000F6F RID: 3951
		[DomName("play")]
		void Play();

		// Token: 0x06000F70 RID: 3952
		[DomName("pause")]
		void Pause();

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F71 RID: 3953
		[DomName("readyState")]
		MediaReadyState ReadyState { get; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F72 RID: 3954
		[DomName("playbackState")]
		MediaControllerPlaybackState PlaybackState { get; }

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06000F73 RID: 3955
		// (remove) Token: 0x06000F74 RID: 3956
		[DomName("onemptied")]
		event DomEventHandler Emptied;

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06000F75 RID: 3957
		// (remove) Token: 0x06000F76 RID: 3958
		[DomName("onloadedmetadata")]
		event DomEventHandler LoadedMetadata;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06000F77 RID: 3959
		// (remove) Token: 0x06000F78 RID: 3960
		[DomName("onloadeddata")]
		event DomEventHandler LoadedData;

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06000F79 RID: 3961
		// (remove) Token: 0x06000F7A RID: 3962
		[DomName("oncanplay")]
		event DomEventHandler CanPlay;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06000F7B RID: 3963
		// (remove) Token: 0x06000F7C RID: 3964
		[DomName("oncanplaythrough")]
		event DomEventHandler CanPlayThrough;

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06000F7D RID: 3965
		// (remove) Token: 0x06000F7E RID: 3966
		[DomName("onended")]
		event DomEventHandler Ended;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06000F7F RID: 3967
		// (remove) Token: 0x06000F80 RID: 3968
		[DomName("onwaiting")]
		event DomEventHandler Waiting;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06000F81 RID: 3969
		// (remove) Token: 0x06000F82 RID: 3970
		[DomName("ondurationchange")]
		event DomEventHandler DurationChanged;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06000F83 RID: 3971
		// (remove) Token: 0x06000F84 RID: 3972
		[DomName("ontimeupdate")]
		event DomEventHandler TimeUpdated;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x06000F85 RID: 3973
		// (remove) Token: 0x06000F86 RID: 3974
		[DomName("onpause")]
		event DomEventHandler Paused;

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06000F87 RID: 3975
		// (remove) Token: 0x06000F88 RID: 3976
		[DomName("onplay")]
		event DomEventHandler Played;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06000F89 RID: 3977
		// (remove) Token: 0x06000F8A RID: 3978
		[DomName("onplaying")]
		event DomEventHandler Playing;

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06000F8B RID: 3979
		// (remove) Token: 0x06000F8C RID: 3980
		[DomName("onratechange")]
		event DomEventHandler RateChanged;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06000F8D RID: 3981
		// (remove) Token: 0x06000F8E RID: 3982
		[DomName("onvolumechange")]
		event DomEventHandler VolumeChanged;
	}
}
