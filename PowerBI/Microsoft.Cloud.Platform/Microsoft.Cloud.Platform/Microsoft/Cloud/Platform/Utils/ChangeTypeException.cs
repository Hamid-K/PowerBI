using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000311 RID: 785
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ChangeTypeException : MemberInfoUtilsException
	{
		// Token: 0x060015FE RID: 5630 RVA: 0x0004ECBC File Offset: 0x0004CEBC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0004ECC4 File Offset: 0x0004CEC4
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0004ECCC File Offset: 0x0004CECC
		public Type TargetType
		{
			get
			{
				return this.m_targetType;
			}
			protected set
			{
				this.m_targetType = value;
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0004ECD5 File Offset: 0x0004CED5
		public ChangeTypeException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Type>();
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0004ECE9 File Offset: 0x0004CEE9
		public ChangeTypeException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004ED00 File Offset: 0x0004CF00
		public ChangeTypeException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0004ED20 File Offset: 0x0004CF20
		protected ChangeTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ChangeTypeException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.TargetType = (Type)info.GetValue("ChangeTypeException_TargetType", typeof(Type));
			}
			catch (SerializationException)
			{
				this.TargetType = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ChangeTypeException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0004EDF4 File Offset: 0x0004CFF4
		public ChangeTypeException(Type targetType)
		{
			this.TargetType = targetType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0004EE0A File Offset: 0x0004D00A
		public ChangeTypeException(Type targetType, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.TargetType = targetType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0004EE28 File Offset: 0x0004D028
		public ChangeTypeException(Type targetType, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.TargetType = targetType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0004EE4C File Offset: 0x0004D04C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0004EE83 File Offset: 0x0004D083
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0004EE8C File Offset: 0x0004D08C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ChangeTypeException))
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

		// Token: 0x0600160B RID: 5643 RVA: 0x0004EF5C File Offset: 0x0004D15C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ChangeTypeException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ChangeTypeException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.TargetType != null)
			{
				info.AddValue("ChangeTypeException_TargetType", this.TargetType, typeof(Type));
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Can't change type from string to {0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.TargetType != null) ? this.TargetType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.TargetType != null) ? this.TargetType.MarkIfInternal() : string.Empty) : ((this.TargetType != null) ? this.TargetType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0004F076 File Offset: 0x0004D276
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

		// Token: 0x0600160E RID: 5646 RVA: 0x0004F094 File Offset: 0x0004D294
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "TargetType={0}", new object[] { (this.TargetType != null) ? this.TargetType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "TargetType={0}", new object[] { (this.TargetType != null) ? this.TargetType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "TargetType={0}", new object[] { (this.TargetType != null) ? this.TargetType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0004F185 File Offset: 0x0004D385
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0004F18E File Offset: 0x0004D38E
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0004F197 File Offset: 0x0004D397
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0004F185 File Offset: 0x0004D385
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0004F1A0 File Offset: 0x0004D3A0
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

		// Token: 0x040007F0 RID: 2032
		private string creationMessage;

		// Token: 0x040007F1 RID: 2033
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007F2 RID: 2034
		private Type m_targetType;
	}
}
