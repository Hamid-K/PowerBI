using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000256 RID: 598
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class SubstituteNonSerializableException : MonitoredException
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00035328 File Offset: 0x00033528
		public override string StackTrace
		{
			get
			{
				return this.InnerCallStack;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00035330 File Offset: 0x00033530
		public override string Message
		{
			get
			{
				return this.InnerMessage;
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00035338 File Offset: 0x00033538
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00035340 File Offset: 0x00033540
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x00035348 File Offset: 0x00033548
		public string InnerType
		{
			get
			{
				return this.m_innerType;
			}
			protected set
			{
				this.m_innerType = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00035351 File Offset: 0x00033551
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x00035359 File Offset: 0x00033559
		public string InnerMessage
		{
			get
			{
				return this.m_innerMessage;
			}
			protected set
			{
				this.m_innerMessage = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00035362 File Offset: 0x00033562
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x0003536A File Offset: 0x0003356A
		public string InnerToString
		{
			get
			{
				return this.m_innerToString;
			}
			protected set
			{
				this.m_innerToString = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00035373 File Offset: 0x00033573
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x0003537B File Offset: 0x0003357B
		public string InnerCallStack
		{
			get
			{
				return this.m_innerCallStack;
			}
			protected set
			{
				this.m_innerCallStack = value;
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00035384 File Offset: 0x00033584
		public SubstituteNonSerializableException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000353A7 File Offset: 0x000335A7
		public SubstituteNonSerializableException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000353BE File Offset: 0x000335BE
		public SubstituteNonSerializableException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000353DC File Offset: 0x000335DC
		protected SubstituteNonSerializableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SubstituteNonSerializableException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.InnerType = (string)info.GetValue("SubstituteNonSerializableException_InnerType", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerType = null;
			}
			try
			{
				this.InnerMessage = (string)info.GetValue("SubstituteNonSerializableException_InnerMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerMessage = null;
			}
			try
			{
				this.InnerToString = (string)info.GetValue("SubstituteNonSerializableException_InnerToString", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerToString = null;
			}
			try
			{
				this.InnerCallStack = (string)info.GetValue("SubstituteNonSerializableException_InnerCallStack", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerCallStack = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("SubstituteNonSerializableException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0003555C File Offset: 0x0003375C
		public SubstituteNonSerializableException(string innerType, string innerMessage, string innerToString, string innerCallStack)
		{
			this.InnerType = innerType;
			this.InnerMessage = innerMessage;
			this.InnerToString = innerToString;
			this.InnerCallStack = innerCallStack;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00035588 File Offset: 0x00033788
		public SubstituteNonSerializableException(string innerType, string innerMessage, string innerToString, string innerCallStack, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.InnerType = innerType;
			this.InnerMessage = innerMessage;
			this.InnerToString = innerToString;
			this.InnerCallStack = innerCallStack;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000355BE File Offset: 0x000337BE
		public SubstituteNonSerializableException(string innerType, string innerMessage, string innerToString, string innerCallStack, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.InnerType = innerType;
			this.InnerMessage = innerMessage;
			this.InnerToString = innerToString;
			this.InnerCallStack = innerCallStack;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000355FC File Offset: 0x000337FC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00035634 File Offset: 0x00033834
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SubstituteNonSerializableException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("SubstituteNonSerializableException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.InnerType != null)
			{
				info.AddValue("SubstituteNonSerializableException_InnerType", this.InnerType, typeof(string));
			}
			if (this.InnerMessage != null)
			{
				info.AddValue("SubstituteNonSerializableException_InnerMessage", this.InnerMessage, typeof(string));
			}
			if (this.InnerToString != null)
			{
				info.AddValue("SubstituteNonSerializableException_InnerToString", this.InnerToString, typeof(string));
			}
			if (this.InnerCallStack != null)
			{
				info.AddValue("SubstituteNonSerializableException_InnerCallStack", this.InnerCallStack, typeof(string));
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003571C File Offset: 0x0003391C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Substituted: {0}:{1}", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.InnerType != null) ? this.InnerType.MarkIfInternal() : string.Empty) : ((this.InnerType != null) ? this.InnerType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.InnerToString != null) ? this.InnerToString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.InnerToString != null) ? this.InnerToString.MarkIfInternal() : string.Empty) : ((this.InnerToString != null) ? this.InnerToString.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00035804 File Offset: 0x00033A04
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerType={0}", new object[] { (this.InnerType != null) ? this.InnerType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerType={0}", new object[] { (this.InnerType != null) ? this.InnerType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "InnerType={0}", new object[] { (this.InnerType != null) ? this.InnerType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", new object[] { (this.InnerMessage != null) ? this.InnerMessage.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", new object[] { (this.InnerMessage != null) ? this.InnerMessage.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", new object[] { (this.InnerMessage != null) ? this.InnerMessage.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", new object[] { (this.InnerToString != null) ? this.InnerToString.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", new object[] { (this.InnerToString != null) ? this.InnerToString.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", new object[] { (this.InnerToString != null) ? this.InnerToString.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", new object[] { (this.InnerCallStack != null) ? this.InnerCallStack.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", new object[] { (this.InnerCallStack != null) ? this.InnerCallStack.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", new object[] { (this.InnerCallStack != null) ? this.InnerCallStack.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00035B2C File Offset: 0x00033D2C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00035B35 File Offset: 0x00033D35
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00035B3E File Offset: 0x00033D3E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00035B2C File Offset: 0x00033D2C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00035B48 File Offset: 0x00033D48
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

		// Token: 0x040005E0 RID: 1504
		private string creationMessage;

		// Token: 0x040005E1 RID: 1505
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040005E2 RID: 1506
		private string m_innerType;

		// Token: 0x040005E3 RID: 1507
		private string m_innerMessage;

		// Token: 0x040005E4 RID: 1508
		private string m_innerToString;

		// Token: 0x040005E5 RID: 1509
		private string m_innerCallStack;
	}
}
