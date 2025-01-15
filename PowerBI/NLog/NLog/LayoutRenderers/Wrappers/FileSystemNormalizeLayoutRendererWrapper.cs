using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000F3 RID: 243
	[LayoutRenderer("filesystem-normalize")]
	[AmbientProperty("FSNormalize")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class FileSystemNormalizeLayoutRendererWrapper : WrapperLayoutRendererBuilderBase
	{
		// Token: 0x06000DC0 RID: 3520 RVA: 0x00022A98 File Offset: 0x00020C98
		public FileSystemNormalizeLayoutRendererWrapper()
		{
			this.FSNormalize = true;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00022AA7 File Offset: 0x00020CA7
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00022AAF File Offset: 0x00020CAF
		[DefaultValue(true)]
		public bool FSNormalize { get; set; }

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00022AB8 File Offset: 0x00020CB8
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (this.FSNormalize && builder.Length > orgLength)
			{
				FileSystemNormalizeLayoutRendererWrapper.TransformFileSystemNormalize(builder, orgLength);
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00022AE0 File Offset: 0x00020CE0
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00022AE4 File Offset: 0x00020CE4
		private static void TransformFileSystemNormalize(StringBuilder builder, int startPos)
		{
			for (int i = startPos; i < builder.Length; i++)
			{
				if (!FileSystemNormalizeLayoutRendererWrapper.IsSafeCharacter(builder[i]))
				{
					builder[i] = '_';
				}
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00022B19 File Offset: 0x00020D19
		private static bool IsSafeCharacter(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == '.' || c == ' ';
		}
	}
}
