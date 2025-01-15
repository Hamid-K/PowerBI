using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000121 RID: 289
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class BuildPowerBIModelFailedException : PowerBIModelBuilderException
	{
		// Token: 0x06000EFE RID: 3838 RVA: 0x0003BA44 File Offset: 0x00039C44
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003BA4C File Offset: 0x00039C4C
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0003BA54 File Offset: 0x00039C54
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
			protected set
			{
				this.m_databaseName = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0003BA5D File Offset: 0x00039C5D
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0003BA65 File Offset: 0x00039C65
		public string Step
		{
			get
			{
				return this.m_step;
			}
			protected set
			{
				this.m_step = value;
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003BA6E File Offset: 0x00039C6E
		public BuildPowerBIModelFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0003BA87 File Offset: 0x00039C87
		public BuildPowerBIModelFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003BA9E File Offset: 0x00039C9E
		public BuildPowerBIModelFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0003BABC File Offset: 0x00039CBC
		protected BuildPowerBIModelFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("BuildPowerBIModelFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("BuildPowerBIModelFailedException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.Step = (string)info.GetValue("BuildPowerBIModelFailedException_Step", typeof(string));
			}
			catch (SerializationException)
			{
				this.Step = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("BuildPowerBIModelFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003BBCC File Offset: 0x00039DCC
		public BuildPowerBIModelFailedException(string databaseName, string step)
		{
			this.DatabaseName = databaseName;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003BBE9 File Offset: 0x00039DE9
		public BuildPowerBIModelFailedException(string databaseName, string step, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003BC0E File Offset: 0x00039E0E
		public BuildPowerBIModelFailedException(string databaseName, string step, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003BC3C File Offset: 0x00039E3C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003BC74 File Offset: 0x00039E74
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("BuildPowerBIModelFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("BuildPowerBIModelFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("BuildPowerBIModelFailedException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.Step != null)
			{
				info.AddValue("BuildPowerBIModelFailedException_Step", this.Step, typeof(string));
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003BD18 File Offset: 0x00039F18
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "BuildPowerBIModel for database '{0}' failed at {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Step != null) ? this.Step.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Step != null) ? this.Step.MarkIfInternal() : string.Empty) : ((this.Step != null) ? this.Step.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0003BDF2 File Offset: 0x00039FF2
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(this.creationMessage))
				{
					return this.creationMessage;
				}
				return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.None);
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003BE10 File Offset: 0x0003A010
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003BF7C File Offset: 0x0003A17C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0003BF85 File Offset: 0x0003A185
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003BF8E File Offset: 0x0003A18E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003BF7C File Offset: 0x0003A17C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003BF98 File Offset: 0x0003A198
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
			string text2 = this.CreateMessageFromTemplate(markupKind);
			string text3 = text + ": ";
			if (string.IsNullOrEmpty(this.creationMessage))
			{
				text3 += text2;
			}
			else
			{
				if (markupKind == PrivateInformationMarkupKind.Private || markupKind == PrivateInformationMarkupKind.Internal)
				{
					text3 += this.creationMessage.ObfuscatePrivateValue(true);
				}
				else
				{
					text3 += this.creationMessage;
				}
				if (!string.Equals(this.creationMessage, text2))
				{
					text3 = text3 + Environment.NewLine + "  TemplateMessage: " + text2;
				}
			}
			text3 += this.GetPropertiesString(markupKind);
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x04000387 RID: 903
		private string creationMessage;

		// Token: 0x04000388 RID: 904
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000389 RID: 905
		private string m_databaseName;

		// Token: 0x0400038A RID: 906
		private string m_step;
	}
}
