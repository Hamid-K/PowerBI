using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.KeyValueStore
{
	// Token: 0x02000138 RID: 312
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class KeyValueDoesNotExists : MonitoredException
	{
		// Token: 0x060010DB RID: 4315 RVA: 0x00044914 File Offset: 0x00042B14
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0004491C File Offset: 0x00042B1C
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x00044924 File Offset: 0x00042B24
		public string Key
		{
			get
			{
				return this.m_key;
			}
			protected set
			{
				this.m_key = value;
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004492D File Offset: 0x00042B2D
		public KeyValueDoesNotExists()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00044941 File Offset: 0x00042B41
		public KeyValueDoesNotExists(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00044958 File Offset: 0x00042B58
		public KeyValueDoesNotExists(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00044978 File Offset: 0x00042B78
		protected KeyValueDoesNotExists(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("KeyValueDoesNotExists_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Key = (string)info.GetValue("KeyValueDoesNotExists_Key", typeof(string));
			}
			catch (SerializationException)
			{
				this.Key = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("KeyValueDoesNotExists_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00044A4C File Offset: 0x00042C4C
		public KeyValueDoesNotExists(string key, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Key = key;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00044A6A File Offset: 0x00042C6A
		public KeyValueDoesNotExists(string key, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Key = key;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00044A90 File Offset: 0x00042C90
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00044AC7 File Offset: 0x00042CC7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00044AD0 File Offset: 0x00042CD0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(KeyValueDoesNotExists))
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

		// Token: 0x060010E7 RID: 4327 RVA: 0x00044BA0 File Offset: 0x00042DA0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("KeyValueDoesNotExists_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("KeyValueDoesNotExists_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Key != null)
			{
				info.AddValue("KeyValueDoesNotExists_Key", this.Key, typeof(string));
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00044C20 File Offset: 0x00042E20
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Key Value {0} Doesn't Exists", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Key != null) ? this.Key.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Key != null) ? this.Key.MarkIfInternal() : string.Empty) : ((this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00044C9B File Offset: 0x00042E9B
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

		// Token: 0x060010EA RID: 4330 RVA: 0x00044CB8 File Offset: 0x00042EB8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", (this.Key != null) ? this.Key.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", (this.Key != null) ? this.Key.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Key={0}", (this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00044D7C File Offset: 0x00042F7C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00044D85 File Offset: 0x00042F85
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00044D8E File Offset: 0x00042F8E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00044D7C File Offset: 0x00042F7C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00044D98 File Offset: 0x00042F98
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

		// Token: 0x040003CE RID: 974
		private string creationMessage;

		// Token: 0x040003CF RID: 975
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003D0 RID: 976
		private string m_key;
	}
}
