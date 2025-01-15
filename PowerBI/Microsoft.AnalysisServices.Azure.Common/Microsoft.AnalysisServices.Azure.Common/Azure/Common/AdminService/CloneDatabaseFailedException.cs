using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x0200012F RID: 303
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CloneDatabaseFailedException : AdminProvisioningServiceException
	{
		// Token: 0x06001025 RID: 4133 RVA: 0x0004133C File Offset: 0x0003F53C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00041344 File Offset: 0x0003F544
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0004134C File Offset: 0x0003F54C
		public string SourceDatabaseName
		{
			get
			{
				return this.m_sourceDatabaseName;
			}
			protected set
			{
				this.m_sourceDatabaseName = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00041355 File Offset: 0x0003F555
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0004135D File Offset: 0x0003F55D
		public string TargetDatabaseName
		{
			get
			{
				return this.m_targetDatabaseName;
			}
			protected set
			{
				this.m_targetDatabaseName = value;
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00041366 File Offset: 0x0003F566
		public CloneDatabaseFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0004137F File Offset: 0x0003F57F
		public CloneDatabaseFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00041396 File Offset: 0x0003F596
		public CloneDatabaseFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000413B4 File Offset: 0x0003F5B4
		protected CloneDatabaseFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CloneDatabaseFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.SourceDatabaseName = (string)info.GetValue("CloneDatabaseFailedException_SourceDatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.SourceDatabaseName = null;
			}
			try
			{
				this.TargetDatabaseName = (string)info.GetValue("CloneDatabaseFailedException_TargetDatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.TargetDatabaseName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CloneDatabaseFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000414C4 File Offset: 0x0003F6C4
		public CloneDatabaseFailedException(string sourceDatabaseName, string targetDatabaseName)
		{
			this.SourceDatabaseName = sourceDatabaseName;
			this.TargetDatabaseName = targetDatabaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000414E1 File Offset: 0x0003F6E1
		public CloneDatabaseFailedException(string sourceDatabaseName, string targetDatabaseName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.SourceDatabaseName = sourceDatabaseName;
			this.TargetDatabaseName = targetDatabaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00041506 File Offset: 0x0003F706
		public CloneDatabaseFailedException(string sourceDatabaseName, string targetDatabaseName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.SourceDatabaseName = sourceDatabaseName;
			this.TargetDatabaseName = targetDatabaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00041534 File Offset: 0x0003F734
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004156B File Offset: 0x0003F76B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00041574 File Offset: 0x0003F774
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CloneDatabaseFailedException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00041644 File Offset: 0x0003F844
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CloneDatabaseFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CloneDatabaseFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.SourceDatabaseName != null)
			{
				info.AddValue("CloneDatabaseFailedException_SourceDatabaseName", this.SourceDatabaseName, typeof(string));
			}
			if (this.TargetDatabaseName != null)
			{
				info.AddValue("CloneDatabaseFailedException_TargetDatabaseName", this.TargetDatabaseName, typeof(string));
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000416E8 File Offset: 0x0003F8E8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Clone source database '{0}' to target database '{1}' failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.SourceDatabaseName != null) ? this.SourceDatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.SourceDatabaseName != null) ? this.SourceDatabaseName.MarkIfInternal() : string.Empty) : ((this.SourceDatabaseName != null) ? this.SourceDatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.TargetDatabaseName != null) ? this.TargetDatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.TargetDatabaseName != null) ? this.TargetDatabaseName.MarkIfInternal() : string.Empty) : ((this.TargetDatabaseName != null) ? this.TargetDatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x000417C2 File Offset: 0x0003F9C2
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

		// Token: 0x06001037 RID: 4151 RVA: 0x000417E0 File Offset: 0x0003F9E0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "SourceDatabaseName={0}", (this.SourceDatabaseName != null) ? this.SourceDatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "SourceDatabaseName={0}", (this.SourceDatabaseName != null) ? this.SourceDatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "SourceDatabaseName={0}", (this.SourceDatabaseName != null) ? this.SourceDatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "TargetDatabaseName={0}", (this.TargetDatabaseName != null) ? this.TargetDatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "TargetDatabaseName={0}", (this.TargetDatabaseName != null) ? this.TargetDatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "TargetDatabaseName={0}", (this.TargetDatabaseName != null) ? this.TargetDatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0004194C File Offset: 0x0003FB4C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00041955 File Offset: 0x0003FB55
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004195E File Offset: 0x0003FB5E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0004194C File Offset: 0x0003FB4C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00041968 File Offset: 0x0003FB68
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

		// Token: 0x040003B4 RID: 948
		private string creationMessage;

		// Token: 0x040003B5 RID: 949
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003B6 RID: 950
		private string m_sourceDatabaseName;

		// Token: 0x040003B7 RID: 951
		private string m_targetDatabaseName;
	}
}
