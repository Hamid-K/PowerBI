using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;
using AngleSharp.Services.Media;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000370 RID: 880
	internal abstract class HtmlMediaElement<TResource> : HtmlElement, IHtmlMediaElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IMediaController, ILoadableElement where TResource : class, IMediaInfo
	{
		// Token: 0x06001B8B RID: 7051 RVA: 0x00053606 File Offset: 0x00051806
		public HtmlMediaElement(Document owner, string name, string prefix)
			: base(owner, name, prefix, NodeFlags.None)
		{
			this._request = MediaRequestProcessor<TResource>.Create(this);
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0005361E File Offset: 0x0005181E
		public IDownload CurrentDownload
		{
			get
			{
				MediaRequestProcessor<TResource> request = this._request;
				if (request == null)
				{
					return null;
				}
				return request.Download;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x000524DE File Offset: 0x000506DE
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00051A18 File Offset: 0x0004FC18
		public string Source
		{
			get
			{
				return this.GetUrlAttribute(AttributeNames.Src);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Src, value, false);
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x000528DE File Offset: 0x00050ADE
		// (set) Token: 0x06001B90 RID: 7056 RVA: 0x000528EB File Offset: 0x00050AEB
		public string CrossOrigin
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.CrossOrigin);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.CrossOrigin, value, false);
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x00053631 File Offset: 0x00051831
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x0005363E File Offset: 0x0005183E
		public string Preload
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Preload);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Preload, value, false);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0005364D File Offset: 0x0005184D
		public MediaNetworkState NetworkState
		{
			get
			{
				MediaRequestProcessor<TResource> request = this._request;
				if (request == null)
				{
					return MediaNetworkState.Empty;
				}
				return request.NetworkState;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00053660 File Offset: 0x00051860
		public TResource Media
		{
			get
			{
				MediaRequestProcessor<TResource> request = this._request;
				if (request == null)
				{
					return default(TResource);
				}
				return request.Resource;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00053688 File Offset: 0x00051888
		public MediaReadyState ReadyState
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller != null)
				{
					return controller.ReadyState;
				}
				return MediaReadyState.Nothing;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000536A7 File Offset: 0x000518A7
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x000536AF File Offset: 0x000518AF
		public bool IsSeeking { get; protected set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000536B8 File Offset: 0x000518B8
		public string CurrentSource
		{
			get
			{
				return this.Source;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000536C0 File Offset: 0x000518C0
		public double Duration
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return 0.0;
				}
				return controller.Duration;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000536DB File Offset: 0x000518DB
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x000536F8 File Offset: 0x000518F8
		public double CurrentTime
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return 0.0;
				}
				return controller.CurrentTime;
			}
			set
			{
				IMediaController controller = this.Controller;
				if (controller != null)
				{
					controller.CurrentTime = value;
				}
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00053716 File Offset: 0x00051916
		// (set) Token: 0x06001B9D RID: 7069 RVA: 0x00053723 File Offset: 0x00051923
		public bool IsAutoplay
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Autoplay);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Autoplay, value);
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x00053731 File Offset: 0x00051931
		// (set) Token: 0x06001B9F RID: 7071 RVA: 0x0005373E File Offset: 0x0005193E
		public bool IsLoop
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Loop);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Loop, value);
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0005374C File Offset: 0x0005194C
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x00053759 File Offset: 0x00051959
		public bool IsShowingControls
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Controls);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Controls, value);
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00053767 File Offset: 0x00051967
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x00053774 File Offset: 0x00051974
		public bool IsDefaultMuted
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Muted);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Muted, value);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00053782 File Offset: 0x00051982
		public bool IsPaused
		{
			get
			{
				return this.PlaybackState == MediaControllerPlaybackState.Waiting && this.ReadyState >= MediaReadyState.CurrentData;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0005379A File Offset: 0x0005199A
		public bool IsEnded
		{
			get
			{
				return this.PlaybackState == MediaControllerPlaybackState.Ended;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x000537A5 File Offset: 0x000519A5
		public DateTime StartDate
		{
			get
			{
				return DateTime.Today;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000537AC File Offset: 0x000519AC
		public ITimeRanges BufferedTime
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return null;
				}
				return controller.BufferedTime;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000537BF File Offset: 0x000519BF
		public ITimeRanges SeekableTime
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return null;
				}
				return controller.SeekableTime;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x000537D2 File Offset: 0x000519D2
		public ITimeRanges PlayedTime
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return null;
				}
				return controller.PlayedTime;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000537E5 File Offset: 0x000519E5
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x000537F2 File Offset: 0x000519F2
		public string MediaGroup
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.MediaGroup);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.MediaGroup, value, false);
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00053801 File Offset: 0x00051A01
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x0005381C File Offset: 0x00051A1C
		public double Volume
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return 1.0;
				}
				return controller.Volume;
			}
			set
			{
				IMediaController controller = this.Controller;
				if (controller != null)
				{
					controller.Volume = value;
				}
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0005383A File Offset: 0x00051A3A
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x00053850 File Offset: 0x00051A50
		public bool IsMuted
		{
			get
			{
				IMediaController controller = this.Controller;
				return controller != null && controller.IsMuted;
			}
			set
			{
				IMediaController controller = this.Controller;
				if (controller != null)
				{
					controller.IsMuted = value;
				}
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0005386E File Offset: 0x00051A6E
		public IMediaController Controller
		{
			get
			{
				MediaRequestProcessor<TResource> request = this._request;
				if (request == null)
				{
					return null;
				}
				TResource tresource = request.Resource;
				if (tresource == null)
				{
					return null;
				}
				return tresource.Controller;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x00053891 File Offset: 0x00051A91
		// (set) Token: 0x06001BB2 RID: 7090 RVA: 0x000538AC File Offset: 0x00051AAC
		public double DefaultPlaybackRate
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return 1.0;
				}
				return controller.DefaultPlaybackRate;
			}
			set
			{
				IMediaController controller = this.Controller;
				if (controller != null)
				{
					controller.DefaultPlaybackRate = value;
				}
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x000538CA File Offset: 0x00051ACA
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x000538E8 File Offset: 0x00051AE8
		public double PlaybackRate
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return 1.0;
				}
				return controller.PlaybackRate;
			}
			set
			{
				IMediaController controller = this.Controller;
				if (controller != null)
				{
					controller.PlaybackRate = value;
				}
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00053906 File Offset: 0x00051B06
		public MediaControllerPlaybackState PlaybackState
		{
			get
			{
				IMediaController controller = this.Controller;
				if (controller == null)
				{
					return MediaControllerPlaybackState.Waiting;
				}
				return controller.PlaybackState;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00053919 File Offset: 0x00051B19
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x00053921 File Offset: 0x00051B21
		public IMediaError MediaError { get; private set; }

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0000C295 File Offset: 0x0000A495
		public virtual IAudioTrackList AudioTracks
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0000C295 File Offset: 0x0000A495
		public virtual IVideoTrackList VideoTracks
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0005392A File Offset: 0x00051B2A
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x00053932 File Offset: 0x00051B32
		public ITextTrackList TextTracks
		{
			get
			{
				return this._texts;
			}
			protected set
			{
				this._texts = value;
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0005393C File Offset: 0x00051B3C
		public void Load()
		{
			string currentSource = this.CurrentSource;
			this.UpdateSource(currentSource);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00053957 File Offset: 0x00051B57
		public void Play()
		{
			IMediaController controller = this.Controller;
			if (controller == null)
			{
				return;
			}
			controller.Play();
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00053969 File Offset: 0x00051B69
		public void Pause()
		{
			IMediaController controller = this.Controller;
			if (controller == null)
			{
				return;
			}
			controller.Pause();
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0005397B File Offset: 0x00051B7B
		public string CanPlayType(string type)
		{
			Document owner = base.Owner;
			if (((owner != null) ? owner.Options.GetResourceService(type) : null) == null)
			{
				return string.Empty;
			}
			return "maybe";
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0000C295 File Offset: 0x0000A495
		public ITextTrack AddTextTrack(string kind, string label = null, string language = null)
		{
			return null;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000539A4 File Offset: 0x00051BA4
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Src);
			if (ownAttribute != null)
			{
				this.UpdateSource(ownAttribute);
			}
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000539D0 File Offset: 0x00051BD0
		internal void UpdateSource(string value)
		{
			Url url = new Url(value);
			this.Process(this._request, url);
		}

		// Token: 0x04000CE8 RID: 3304
		private readonly MediaRequestProcessor<TResource> _request;

		// Token: 0x04000CE9 RID: 3305
		private ITextTrackList _texts;
	}
}
