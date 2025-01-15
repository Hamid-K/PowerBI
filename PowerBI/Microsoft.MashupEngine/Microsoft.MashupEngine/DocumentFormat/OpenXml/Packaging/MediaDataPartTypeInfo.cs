using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002100 RID: 8448
	internal static class MediaDataPartTypeInfo
	{
		// Token: 0x0600D02D RID: 53293 RVA: 0x00295DDC File Offset: 0x00293FDC
		internal static string GetContentType(this MediaDataPartType mediaDataPartType)
		{
			switch (mediaDataPartType)
			{
			case MediaDataPartType.Aiff:
				return "audio/aiff";
			case MediaDataPartType.Midi:
				return "audio/midi";
			case MediaDataPartType.Mp3:
				return "audio/mp3";
			case MediaDataPartType.MpegUrl:
				return "audio/mpegurl";
			case MediaDataPartType.Wav:
				return "audio/wav";
			case MediaDataPartType.Wma:
				return "audio/x-ms-wma";
			case MediaDataPartType.MpegAudio:
				return "audio/mpeg";
			case MediaDataPartType.OggAudio:
				return "audio/ogg";
			case MediaDataPartType.Asx:
				return "video/x-ms-asf-plugin";
			case MediaDataPartType.Avi:
				return "video/avi";
			case MediaDataPartType.Mpg:
				return "video/mpg";
			case MediaDataPartType.MpegVideo:
				return "video/mpeg";
			case MediaDataPartType.Wmv:
				return "video/x-ms-wmv";
			case MediaDataPartType.Wmx:
				return "video/x-ms-wmx";
			case MediaDataPartType.Wvx:
				return "video/x-ms-wvx";
			case MediaDataPartType.Quicktime:
				return "video/quicktime";
			case MediaDataPartType.OggVideo:
				return "video/ogg";
			case MediaDataPartType.VC1:
				return "video/vc1";
			default:
				throw new ArgumentOutOfRangeException("mediaDataPartType");
			}
		}

		// Token: 0x0600D02E RID: 53294 RVA: 0x00295EB4 File Offset: 0x002940B4
		internal static string GetTargetExtension(this MediaDataPartType mediaDataPartType)
		{
			switch (mediaDataPartType)
			{
			case MediaDataPartType.Aiff:
				return ".aiff";
			case MediaDataPartType.Midi:
				return ".midi";
			case MediaDataPartType.Mp3:
				return ".mp3";
			case MediaDataPartType.MpegUrl:
				return ".m3u";
			case MediaDataPartType.Wav:
				return ".wav";
			case MediaDataPartType.Wma:
				return ".wma";
			case MediaDataPartType.MpegAudio:
				return ".mpeg";
			case MediaDataPartType.OggAudio:
				return ".ogg";
			case MediaDataPartType.Asx:
				return ".asx";
			case MediaDataPartType.Avi:
				return ".avi";
			case MediaDataPartType.Mpg:
				return ".mpg";
			case MediaDataPartType.MpegVideo:
				return ".mpeg";
			case MediaDataPartType.Wmv:
				return ".wmv";
			case MediaDataPartType.Wmx:
				return ".wmx";
			case MediaDataPartType.Wvx:
				return ".wvx";
			case MediaDataPartType.Quicktime:
				return ".mov";
			case MediaDataPartType.OggVideo:
				return ".ogg";
			case MediaDataPartType.VC1:
				return ".wmv";
			default:
				return ".media";
			}
		}
	}
}
