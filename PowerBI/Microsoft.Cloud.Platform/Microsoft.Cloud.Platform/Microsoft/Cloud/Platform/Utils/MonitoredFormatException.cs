using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000255 RID: 597
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MonitoredFormatException : FormatException, IMonitoredError, IContainsPrivateInformation
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x00034C13 File Offset: 0x00032E13
		private void Constructor(bool deserializing)
		{
			if (!deserializing)
			{
				this.ExceptionCorrelation = new ExceptionCorrelation();
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsFatal()
		{
			return false;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsBenign()
		{
			return false;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsPermanent()
		{
			return false;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00025E57 File Offset: 0x00024057
		public string ErrorShortName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00034C23 File Offset: 0x00032E23
		public ErrorCorrelationId ErrorCorrelationId
		{
			get
			{
				return this.ExceptionCorrelation.ErrorCorrelationId;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00034C30 File Offset: 0x00032E30
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x00034C3D File Offset: 0x00032E3D
		public MonitoringScopeId MonitoringScope
		{
			get
			{
				return this.ExceptionCorrelation.MonitoringScope;
			}
			set
			{
				this.ExceptionCorrelation.MonitoringScope = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00034C4B File Offset: 0x00032E4B
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x00034C58 File Offset: 0x00032E58
		public long ErrorEventId
		{
			get
			{
				return this.ExceptionCorrelation.ErrorEventId;
			}
			set
			{
				this.ExceptionCorrelation.ErrorEventId = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00034C66 File Offset: 0x00032E66
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x00034C73 File Offset: 0x00032E73
		public long ErrorEventsKitId
		{
			get
			{
				return this.ExceptionCorrelation.ErrorEventsKitId;
			}
			set
			{
				this.ExceptionCorrelation.ErrorEventsKitId = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x00034C81 File Offset: 0x00032E81
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x00034C8E File Offset: 0x00032E8E
		public string ErrorEventName
		{
			get
			{
				return this.ExceptionCorrelation.ErrorEventName;
			}
			set
			{
				this.ExceptionCorrelation.ErrorEventName = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00034C9C File Offset: 0x00032E9C
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x00034CA9 File Offset: 0x00032EA9
		public string ErrorEventsKitName
		{
			get
			{
				return this.ExceptionCorrelation.ErrorEventsKitName;
			}
			set
			{
				this.ExceptionCorrelation.ErrorEventsKitName = value;
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00034CB7 File Offset: 0x00032EB7
		public virtual ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00034CBF File Offset: 0x00032EBF
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00034CC7 File Offset: 0x00032EC7
		public ExceptionCorrelation ExceptionCorrelation
		{
			get
			{
				return this.m_exceptionCorrelation;
			}
			protected set
			{
				this.m_exceptionCorrelation = value;
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00034CD0 File Offset: 0x00032ED0
		public MonitoredFormatException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ExceptionCorrelation>();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00034CE4 File Offset: 0x00032EE4
		public MonitoredFormatException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00034CFB File Offset: 0x00032EFB
		public MonitoredFormatException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00034D18 File Offset: 0x00032F18
		protected MonitoredFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MonitoredFormatException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ExceptionCorrelation = (ExceptionCorrelation)info.GetValue("MonitoredFormatException_ExceptionCorrelation", typeof(ExceptionCorrelation));
			}
			catch (SerializationException)
			{
				this.ExceptionCorrelation = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MonitoredFormatException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00034DEC File Offset: 0x00032FEC
		public MonitoredFormatException(ExceptionCorrelation exceptionCorrelation)
		{
			this.ExceptionCorrelation = exceptionCorrelation;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00034E02 File Offset: 0x00033002
		public MonitoredFormatException(ExceptionCorrelation exceptionCorrelation, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ExceptionCorrelation = exceptionCorrelation;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00034E20 File Offset: 0x00033020
		public MonitoredFormatException(ExceptionCorrelation exceptionCorrelation, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ExceptionCorrelation = exceptionCorrelation;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00034E44 File Offset: 0x00033044
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00034E7C File Offset: 0x0003307C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MonitoredFormatException))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00034F4C File Offset: 0x0003314C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MonitoredFormatException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MonitoredFormatException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ExceptionCorrelation != null)
			{
				info.AddValue("MonitoredFormatException_ExceptionCorrelation", this.ExceptionCorrelation, typeof(ExceptionCorrelation));
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00034FCC File Offset: 0x000331CC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? (base.Message ?? string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? (base.Message ?? string.Empty) : (base.Message ?? string.Empty)) });
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00035029 File Offset: 0x00033229
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

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00035048 File Offset: 0x00033248
		protected virtual string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return (string.IsNullOrEmpty(newLine) ? newLine : (newLine + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExceptionCorrelation={0}", new object[] { (this.ExceptionCorrelation != null) ? this.ExceptionCorrelation.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExceptionCorrelation={0}", new object[] { (this.ExceptionCorrelation != null) ? this.ExceptionCorrelation.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ExceptionCorrelation={0}", new object[] { (this.ExceptionCorrelation != null) ? this.ExceptionCorrelation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003511F File Offset: 0x0003331F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00035128 File Offset: 0x00033328
		public virtual string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00035131 File Offset: 0x00033331
		public virtual string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003511F File Offset: 0x0003331F
		public virtual string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003513C File Offset: 0x0003333C
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

		// Token: 0x040005DD RID: 1501
		private string creationMessage;

		// Token: 0x040005DE RID: 1502
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040005DF RID: 1503
		private ExceptionCorrelation m_exceptionCorrelation;
	}
}
