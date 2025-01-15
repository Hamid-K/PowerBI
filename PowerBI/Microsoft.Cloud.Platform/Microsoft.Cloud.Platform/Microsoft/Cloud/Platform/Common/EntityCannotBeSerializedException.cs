using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000555 RID: 1365
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EntityCannotBeSerializedException : EntitySerializerException
	{
		// Token: 0x060029D8 RID: 10712 RVA: 0x00096F38 File Offset: 0x00095138
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x00096F40 File Offset: 0x00095140
		// (set) Token: 0x060029DA RID: 10714 RVA: 0x00096F48 File Offset: 0x00095148
		public Type EntityType
		{
			get
			{
				return this.m_entityType;
			}
			protected set
			{
				this.m_entityType = value;
			}
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x00096F51 File Offset: 0x00095151
		public EntityCannotBeSerializedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Type>();
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x00096F65 File Offset: 0x00095165
		public EntityCannotBeSerializedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x00096F7C File Offset: 0x0009517C
		public EntityCannotBeSerializedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x00096F9C File Offset: 0x0009519C
		protected EntityCannotBeSerializedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EntityCannotBeSerializedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.EntityType = (Type)info.GetValue("EntityCannotBeSerializedException_EntityType", typeof(Type));
			}
			catch (SerializationException)
			{
				this.EntityType = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EntityCannotBeSerializedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x00097070 File Offset: 0x00095270
		public EntityCannotBeSerializedException(Type entityType)
		{
			this.EntityType = entityType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x00097086 File Offset: 0x00095286
		public EntityCannotBeSerializedException(Type entityType, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.EntityType = entityType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000970A4 File Offset: 0x000952A4
		public EntityCannotBeSerializedException(Type entityType, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.EntityType = entityType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000970C8 File Offset: 0x000952C8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000970FF File Offset: 0x000952FF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x00097108 File Offset: 0x00095308
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EntityCannotBeSerializedException))
			{
				TraceSourceBase<CommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<CommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<CommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000971D8 File Offset: 0x000953D8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EntityCannotBeSerializedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EntityCannotBeSerializedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.EntityType != null)
			{
				info.AddValue("EntityCannotBeSerializedException_EntityType", this.EntityType, typeof(Type));
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0009725C File Offset: 0x0009545C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Object of Type: '{0}' cannot be serialized (see inner exception for details)", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.EntityType != null) ? this.EntityType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.EntityType != null) ? this.EntityType.MarkIfInternal() : string.Empty) : ((this.EntityType != null) ? this.EntityType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x000972F2 File Offset: 0x000954F2
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

		// Token: 0x060029E8 RID: 10728 RVA: 0x00097310 File Offset: 0x00095510
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EntityType={0}", new object[] { (this.EntityType != null) ? this.EntityType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EntityType={0}", new object[] { (this.EntityType != null) ? this.EntityType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "EntityType={0}", new object[] { (this.EntityType != null) ? this.EntityType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00097401 File Offset: 0x00095601
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x0009740A File Offset: 0x0009560A
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00097413 File Offset: 0x00095613
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x00097401 File Offset: 0x00095601
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0009741C File Offset: 0x0009561C
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

		// Token: 0x04000EC6 RID: 3782
		private string creationMessage;

		// Token: 0x04000EC7 RID: 3783
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EC8 RID: 3784
		private Type m_entityType;
	}
}
