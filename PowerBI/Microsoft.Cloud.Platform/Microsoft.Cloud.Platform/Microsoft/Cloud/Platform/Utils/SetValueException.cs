using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000312 RID: 786
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class SetValueException : MemberInfoUtilsException
	{
		// Token: 0x06001614 RID: 5652 RVA: 0x0004F38C File Offset: 0x0004D58C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0004F394 File Offset: 0x0004D594
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x0004F39C File Offset: 0x0004D59C
		public string Value
		{
			get
			{
				return this.m_value;
			}
			protected set
			{
				this.m_value = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0004F3A5 File Offset: 0x0004D5A5
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x0004F3AD File Offset: 0x0004D5AD
		public string MemberName
		{
			get
			{
				return this.m_memberName;
			}
			protected set
			{
				this.m_memberName = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0004F3B6 File Offset: 0x0004D5B6
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x0004F3BE File Offset: 0x0004D5BE
		public string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
			protected set
			{
				this.m_objectName = value;
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0004F3C7 File Offset: 0x0004D5C7
		public SetValueException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0004F3E5 File Offset: 0x0004D5E5
		public SetValueException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0004F3FC File Offset: 0x0004D5FC
		public SetValueException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0004F41C File Offset: 0x0004D61C
		protected SetValueException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SetValueException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Value = (string)info.GetValue("SetValueException_Value", typeof(string));
			}
			catch (SerializationException)
			{
				this.Value = null;
			}
			try
			{
				this.MemberName = (string)info.GetValue("SetValueException_MemberName", typeof(string));
			}
			catch (SerializationException)
			{
				this.MemberName = null;
			}
			try
			{
				this.ObjectName = (string)info.GetValue("SetValueException_ObjectName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ObjectName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("SetValueException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0004F564 File Offset: 0x0004D764
		public SetValueException(string value, string memberName, string objectName)
		{
			this.Value = value;
			this.MemberName = memberName;
			this.ObjectName = objectName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0004F588 File Offset: 0x0004D788
		public SetValueException(string value, string memberName, string objectName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Value = value;
			this.MemberName = memberName;
			this.ObjectName = objectName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0004F5B6 File Offset: 0x0004D7B6
		public SetValueException(string value, string memberName, string objectName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Value = value;
			this.MemberName = memberName;
			this.ObjectName = objectName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0004F5EC File Offset: 0x0004D7EC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0004F623 File Offset: 0x0004D823
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0004F62C File Offset: 0x0004D82C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(SetValueException))
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

		// Token: 0x06001625 RID: 5669 RVA: 0x0004F6FC File Offset: 0x0004D8FC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SetValueException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("SetValueException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Value != null)
			{
				info.AddValue("SetValueException_Value", this.Value, typeof(string));
			}
			if (this.MemberName != null)
			{
				info.AddValue("SetValueException_MemberName", this.MemberName, typeof(string));
			}
			if (this.ObjectName != null)
			{
				info.AddValue("SetValueException_ObjectName", this.ObjectName, typeof(string));
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0004F7C0 File Offset: 0x0004D9C0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Can't set the value '{0}' in the member '{1}' of '{2}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Value != null) ? this.Value.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Value != null) ? this.Value.MarkIfInternal() : string.Empty) : ((this.Value != null) ? this.Value.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.MemberName != null) ? this.MemberName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.MemberName != null) ? this.MemberName.MarkIfInternal() : string.Empty) : ((this.MemberName != null) ? this.MemberName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.ObjectName != null) ? this.ObjectName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ObjectName != null) ? this.ObjectName.MarkIfInternal() : string.Empty) : ((this.ObjectName != null) ? this.ObjectName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0004F908 File Offset: 0x0004DB08
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

		// Token: 0x06001628 RID: 5672 RVA: 0x0004F928 File Offset: 0x0004DB28
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Value={0}", new object[] { (this.Value != null) ? this.Value.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Value={0}", new object[] { (this.Value != null) ? this.Value.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Value={0}", new object[] { (this.Value != null) ? this.Value.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "MemberName={0}", new object[] { (this.MemberName != null) ? this.MemberName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "MemberName={0}", new object[] { (this.MemberName != null) ? this.MemberName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "MemberName={0}", new object[] { (this.MemberName != null) ? this.MemberName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ObjectName={0}", new object[] { (this.ObjectName != null) ? this.ObjectName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ObjectName={0}", new object[] { (this.ObjectName != null) ? this.ObjectName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ObjectName={0}", new object[] { (this.ObjectName != null) ? this.ObjectName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0004FB8D File Offset: 0x0004DD8D
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0004FB96 File Offset: 0x0004DD96
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0004FB9F File Offset: 0x0004DD9F
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0004FB8D File Offset: 0x0004DD8D
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0004FBA8 File Offset: 0x0004DDA8
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

		// Token: 0x040007F3 RID: 2035
		private string creationMessage;

		// Token: 0x040007F4 RID: 2036
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007F5 RID: 2037
		private string m_value;

		// Token: 0x040007F6 RID: 2038
		private string m_memberName;

		// Token: 0x040007F7 RID: 2039
		private string m_objectName;
	}
}
