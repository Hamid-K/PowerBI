using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000C7 RID: 199
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class IllegalBlockStateException : MonitoredException
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x00014558 File Offset: 0x00012758
		public IllegalBlockStateException(BlockState state, bool isLegalState)
			: this((isLegalState ? IllegalBlockStateException.sm_legalErrorMessage : IllegalBlockStateException.sm_illegalErrorMessage) + state)
		{
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001457A File Offset: 0x0001277A
		public IllegalBlockStateException(BlockState state, bool isLegalState, string message)
			: this(string.Concat(new object[]
			{
				isLegalState ? IllegalBlockStateException.sm_legalErrorMessage : IllegalBlockStateException.sm_illegalErrorMessage,
				state,
				". ",
				message
			}))
		{
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000145B4 File Offset: 0x000127B4
		public IllegalBlockStateException(Exception inner)
			: base(inner.Message + Environment.NewLine + "*** See inner exception for actual call stack", inner)
		{
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool IsFatal()
		{
			return true;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000145D2 File Offset: 0x000127D2
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000145DA File Offset: 0x000127DA
		public IllegalBlockStateException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000145E9 File Offset: 0x000127E9
		public IllegalBlockStateException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00014600 File Offset: 0x00012800
		public IllegalBlockStateException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00014620 File Offset: 0x00012820
		protected IllegalBlockStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("IllegalBlockStateException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("IllegalBlockStateException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000146BC File Offset: 0x000128BC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000146F3 File Offset: 0x000128F3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000146FC File Offset: 0x000128FC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(IllegalBlockStateException))
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ModularizationFrameworkTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000147CC File Offset: 0x000129CC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("IllegalBlockStateException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("IllegalBlockStateException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00014827 File Offset: 0x00012A27
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The current Block State is illegal for performing the requested action", new object[0]);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001483E File Offset: 0x00012A3E
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

		// Token: 0x060005B7 RID: 1463 RVA: 0x00003A57 File Offset: 0x00001C57
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001485B File Offset: 0x00012A5B
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00014864 File Offset: 0x00012A64
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001486D File Offset: 0x00012A6D
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001485B File Offset: 0x00012A5B
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00014878 File Offset: 0x00012A78
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

		// Token: 0x040001FD RID: 509
		private static string sm_legalErrorMessage = "The requested action can only be performed while the Block State is ";

		// Token: 0x040001FE RID: 510
		private static string sm_illegalErrorMessage = "The requested action cannot be performed while the Block State is ";

		// Token: 0x040001FF RID: 511
		private string creationMessage;

		// Token: 0x04000200 RID: 512
		private ExceptionCulprit exceptionCulprit;
	}
}
