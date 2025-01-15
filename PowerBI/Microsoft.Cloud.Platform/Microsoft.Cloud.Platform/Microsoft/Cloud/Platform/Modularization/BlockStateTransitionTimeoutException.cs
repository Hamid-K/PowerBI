using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000E0 RID: 224
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class BlockStateTransitionTimeoutException : MonitoredException
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x000168C0 File Offset: 0x00014AC0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000168C8 File Offset: 0x00014AC8
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x000168D0 File Offset: 0x00014AD0
		public BlockState StateBefore
		{
			get
			{
				return this.m_stateBefore;
			}
			protected set
			{
				this.m_stateBefore = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000168D9 File Offset: 0x00014AD9
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x000168E1 File Offset: 0x00014AE1
		public BlockState StateDesired
		{
			get
			{
				return this.m_stateDesired;
			}
			protected set
			{
				this.m_stateDesired = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x000168EA File Offset: 0x00014AEA
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x000168F2 File Offset: 0x00014AF2
		public string BlockName
		{
			get
			{
				return this.m_blockName;
			}
			protected set
			{
				this.m_blockName = value;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000168FB File Offset: 0x00014AFB
		public BlockStateTransitionTimeoutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<BlockState>();
			CompileCheck.IsValidValueField<BlockState>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00016919 File Offset: 0x00014B19
		public BlockStateTransitionTimeoutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00016930 File Offset: 0x00014B30
		public BlockStateTransitionTimeoutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00016950 File Offset: 0x00014B50
		protected BlockStateTransitionTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("BlockStateTransitionTimeoutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.StateBefore = (BlockState)info.GetValue("BlockStateTransitionTimeoutException_StateBefore", typeof(BlockState));
			this.StateDesired = (BlockState)info.GetValue("BlockStateTransitionTimeoutException_StateDesired", typeof(BlockState));
			try
			{
				this.BlockName = (string)info.GetValue("BlockStateTransitionTimeoutException_BlockName", typeof(string));
			}
			catch (SerializationException)
			{
				this.BlockName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("BlockStateTransitionTimeoutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00016A64 File Offset: 0x00014C64
		public BlockStateTransitionTimeoutException(BlockState stateBefore, BlockState stateDesired, string blockName)
		{
			this.StateBefore = stateBefore;
			this.StateDesired = stateDesired;
			this.BlockName = blockName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00016A88 File Offset: 0x00014C88
		public BlockStateTransitionTimeoutException(BlockState stateBefore, BlockState stateDesired, string blockName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.StateBefore = stateBefore;
			this.StateDesired = stateDesired;
			this.BlockName = blockName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00016AB6 File Offset: 0x00014CB6
		public BlockStateTransitionTimeoutException(BlockState stateBefore, BlockState stateDesired, string blockName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.StateBefore = stateBefore;
			this.StateDesired = stateDesired;
			this.BlockName = blockName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00016AEC File Offset: 0x00014CEC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00016B23 File Offset: 0x00014D23
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00016B2C File Offset: 0x00014D2C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(BlockStateTransitionTimeoutException))
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

		// Token: 0x0600066B RID: 1643 RVA: 0x00016BFC File Offset: 0x00014DFC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("BlockStateTransitionTimeoutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("BlockStateTransitionTimeoutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("BlockStateTransitionTimeoutException_StateBefore", this.StateBefore, typeof(BlockState));
			info.AddValue("BlockStateTransitionTimeoutException_StateDesired", this.StateDesired, typeof(BlockState));
			if (this.BlockName != null)
			{
				info.AddValue("BlockStateTransitionTimeoutException_BlockName", this.BlockName, typeof(string));
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00016CBC File Offset: 0x00014EBC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "State transition '{0}' -> '{1}' for block '{2}' failed due to timeout. Aborting", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.StateBefore.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.StateBefore.ToString() : this.StateBefore.ToString()),
				(markupKind == PrivateInformationMarkupKind.None) ? this.StateDesired.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.StateDesired.ToString() : this.StateDesired.ToString()),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.BlockName != null) ? this.BlockName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.BlockName != null) ? this.BlockName.MarkIfInternal() : string.Empty) : ((this.BlockName != null) ? this.BlockName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00016DD4 File Offset: 0x00014FD4
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

		// Token: 0x0600066E RID: 1646 RVA: 0x00016DF4 File Offset: 0x00014FF4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "StateBefore={0}", new object[] { this.StateBefore.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "StateBefore={0}", new object[] { this.StateBefore.ToString() }) : string.Format(CultureInfo.CurrentCulture, "StateBefore={0}", new object[] { this.StateBefore.ToString() })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "StateDesired={0}", new object[] { this.StateDesired.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "StateDesired={0}", new object[] { this.StateDesired.ToString() }) : string.Format(CultureInfo.CurrentCulture, "StateDesired={0}", new object[] { this.StateDesired.ToString() })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "BlockName={0}", new object[] { (this.BlockName != null) ? this.BlockName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "BlockName={0}", new object[] { (this.BlockName != null) ? this.BlockName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "BlockName={0}", new object[] { (this.BlockName != null) ? this.BlockName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00017029 File Offset: 0x00015229
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00017032 File Offset: 0x00015232
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001703B File Offset: 0x0001523B
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00017029 File Offset: 0x00015229
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00017044 File Offset: 0x00015244
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

		// Token: 0x04000230 RID: 560
		private string creationMessage;

		// Token: 0x04000231 RID: 561
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000232 RID: 562
		private BlockState m_stateBefore;

		// Token: 0x04000233 RID: 563
		private BlockState m_stateDesired;

		// Token: 0x04000234 RID: 564
		private string m_blockName;
	}
}
