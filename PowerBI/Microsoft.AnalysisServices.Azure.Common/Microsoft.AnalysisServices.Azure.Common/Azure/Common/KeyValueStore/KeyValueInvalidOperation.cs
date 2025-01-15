using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.KeyValueStore
{
	// Token: 0x02000139 RID: 313
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class KeyValueInvalidOperation : MonitoredException
	{
		// Token: 0x060010F0 RID: 4336 RVA: 0x00044F84 File Offset: 0x00043184
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00044F8C File Offset: 0x0004318C
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00044F94 File Offset: 0x00043194
		public string Operation
		{
			get
			{
				return this.m_operation;
			}
			protected set
			{
				this.m_operation = value;
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00044F9D File Offset: 0x0004319D
		public KeyValueInvalidOperation()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00044FB1 File Offset: 0x000431B1
		public KeyValueInvalidOperation(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00044FC8 File Offset: 0x000431C8
		public KeyValueInvalidOperation(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00044FE8 File Offset: 0x000431E8
		protected KeyValueInvalidOperation(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("KeyValueInvalidOperation_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Operation = (string)info.GetValue("KeyValueInvalidOperation_Operation", typeof(string));
			}
			catch (SerializationException)
			{
				this.Operation = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("KeyValueInvalidOperation_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000450BC File Offset: 0x000432BC
		public KeyValueInvalidOperation(string operation, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Operation = operation;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000450DA File Offset: 0x000432DA
		public KeyValueInvalidOperation(string operation, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Operation = operation;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00045100 File Offset: 0x00043300
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00045138 File Offset: 0x00043338
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("KeyValueInvalidOperation_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("KeyValueInvalidOperation_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Operation != null)
			{
				info.AddValue("KeyValueInvalidOperation_Operation", this.Operation, typeof(string));
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x000451B8 File Offset: 0x000433B8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Operation {0} is not supported by this method", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Operation != null) ? this.Operation.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Operation != null) ? this.Operation.MarkIfInternal() : string.Empty) : ((this.Operation != null) ? this.Operation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00045233 File Offset: 0x00043433
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

		// Token: 0x060010FE RID: 4350 RVA: 0x00045250 File Offset: 0x00043450
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00045314 File Offset: 0x00043514
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0004531D File Offset: 0x0004351D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00045326 File Offset: 0x00043526
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00045314 File Offset: 0x00043514
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00045330 File Offset: 0x00043530
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

		// Token: 0x040003D1 RID: 977
		private string creationMessage;

		// Token: 0x040003D2 RID: 978
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003D3 RID: 979
		private string m_operation;
	}
}
