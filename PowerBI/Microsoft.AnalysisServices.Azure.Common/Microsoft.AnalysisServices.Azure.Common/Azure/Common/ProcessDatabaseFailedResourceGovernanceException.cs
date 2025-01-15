using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000E6 RID: 230
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ProcessDatabaseFailedResourceGovernanceException : MonitoredException
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00024A44 File Offset: 0x00022C44
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00024A4C File Offset: 0x00022C4C
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x00024A54 File Offset: 0x00022C54
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

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00024A5D File Offset: 0x00022C5D
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00024A65 File Offset: 0x00022C65
		public string IsOOL
		{
			get
			{
				return this.m_isOOL;
			}
			protected set
			{
				this.m_isOOL = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00024A6E File Offset: 0x00022C6E
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x00024A76 File Offset: 0x00022C76
		public string IsTM
		{
			get
			{
				return this.m_isTM;
			}
			protected set
			{
				this.m_isTM = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00024A7F File Offset: 0x00022C7F
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x00024A87 File Offset: 0x00022C87
		public string MaxParallelism
		{
			get
			{
				return this.m_maxParallelism;
			}
			protected set
			{
				this.m_maxParallelism = value;
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00024A90 File Offset: 0x00022C90
		public ProcessDatabaseFailedResourceGovernanceException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00024AB3 File Offset: 0x00022CB3
		public ProcessDatabaseFailedResourceGovernanceException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00024ACA File Offset: 0x00022CCA
		public ProcessDatabaseFailedResourceGovernanceException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00024AE8 File Offset: 0x00022CE8
		protected ProcessDatabaseFailedResourceGovernanceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ProcessDatabaseFailedResourceGovernanceException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("ProcessDatabaseFailedResourceGovernanceException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.IsOOL = (string)info.GetValue("ProcessDatabaseFailedResourceGovernanceException_IsOOL", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsOOL = null;
			}
			try
			{
				this.IsTM = (string)info.GetValue("ProcessDatabaseFailedResourceGovernanceException_IsTM", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsTM = null;
			}
			try
			{
				this.MaxParallelism = (string)info.GetValue("ProcessDatabaseFailedResourceGovernanceException_MaxParallelism", typeof(string));
			}
			catch (SerializationException)
			{
				this.MaxParallelism = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ProcessDatabaseFailedResourceGovernanceException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00024C68 File Offset: 0x00022E68
		public ProcessDatabaseFailedResourceGovernanceException(string databaseName, string isOOL, string isTM, string maxParallelism)
		{
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.MaxParallelism = maxParallelism;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00024C94 File Offset: 0x00022E94
		public ProcessDatabaseFailedResourceGovernanceException(string databaseName, string isOOL, string isTM, string maxParallelism, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.MaxParallelism = maxParallelism;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00024CCA File Offset: 0x00022ECA
		public ProcessDatabaseFailedResourceGovernanceException(string databaseName, string isOOL, string isTM, string maxParallelism, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.MaxParallelism = maxParallelism;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00024D08 File Offset: 0x00022F08
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00024D40 File Offset: 0x00022F40
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ProcessDatabaseFailedResourceGovernanceException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ProcessDatabaseFailedResourceGovernanceException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("ProcessDatabaseFailedResourceGovernanceException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.IsOOL != null)
			{
				info.AddValue("ProcessDatabaseFailedResourceGovernanceException_IsOOL", this.IsOOL, typeof(string));
			}
			if (this.IsTM != null)
			{
				info.AddValue("ProcessDatabaseFailedResourceGovernanceException_IsTM", this.IsTM, typeof(string));
			}
			if (this.MaxParallelism != null)
			{
				info.AddValue("ProcessDatabaseFailedResourceGovernanceException_MaxParallelism", this.MaxParallelism, typeof(string));
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00024E28 File Offset: 0x00023028
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "ProcessDatabase for database {0} failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00024EA3 File Offset: 0x000230A3
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

		// Token: 0x06000A4F RID: 2639 RVA: 0x00024EC0 File Offset: 0x000230C0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "MaxParallelism={0}", (this.MaxParallelism != null) ? this.MaxParallelism.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "MaxParallelism={0}", (this.MaxParallelism != null) ? this.MaxParallelism.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "MaxParallelism={0}", (this.MaxParallelism != null) ? this.MaxParallelism.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002517C File Offset: 0x0002337C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00025185 File Offset: 0x00023385
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002518E File Offset: 0x0002338E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002517C File Offset: 0x0002337C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00025198 File Offset: 0x00023398
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

		// Token: 0x040002CE RID: 718
		private string creationMessage;

		// Token: 0x040002CF RID: 719
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002D0 RID: 720
		private string m_databaseName;

		// Token: 0x040002D1 RID: 721
		private string m_isOOL;

		// Token: 0x040002D2 RID: 722
		private string m_isTM;

		// Token: 0x040002D3 RID: 723
		private string m_maxParallelism;
	}
}
