using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000554 RID: 1364
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EntityCannotBeDeserializedException : EntitySerializerException
	{
		// Token: 0x060029C2 RID: 10690 RVA: 0x00096868 File Offset: 0x00094A68
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x00096870 File Offset: 0x00094A70
		// (set) Token: 0x060029C4 RID: 10692 RVA: 0x00096878 File Offset: 0x00094A78
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

		// Token: 0x060029C5 RID: 10693 RVA: 0x00096881 File Offset: 0x00094A81
		public EntityCannotBeDeserializedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Type>();
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00096895 File Offset: 0x00094A95
		public EntityCannotBeDeserializedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000968AC File Offset: 0x00094AAC
		public EntityCannotBeDeserializedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000968CC File Offset: 0x00094ACC
		protected EntityCannotBeDeserializedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EntityCannotBeDeserializedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.EntityType = (Type)info.GetValue("EntityCannotBeDeserializedException_EntityType", typeof(Type));
			}
			catch (SerializationException)
			{
				this.EntityType = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EntityCannotBeDeserializedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000969A0 File Offset: 0x00094BA0
		public EntityCannotBeDeserializedException(Type entityType)
		{
			this.EntityType = entityType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000969B6 File Offset: 0x00094BB6
		public EntityCannotBeDeserializedException(Type entityType, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.EntityType = entityType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000969D4 File Offset: 0x00094BD4
		public EntityCannotBeDeserializedException(Type entityType, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.EntityType = entityType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000969F8 File Offset: 0x00094BF8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00096A2F File Offset: 0x00094C2F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x00096A38 File Offset: 0x00094C38
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EntityCannotBeDeserializedException))
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

		// Token: 0x060029CF RID: 10703 RVA: 0x00096B08 File Offset: 0x00094D08
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EntityCannotBeDeserializedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EntityCannotBeDeserializedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.EntityType != null)
			{
				info.AddValue("EntityCannotBeDeserializedException_EntityType", this.EntityType, typeof(Type));
			}
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00096B8C File Offset: 0x00094D8C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Object of Type: '{0}' cannot be deserialized (see inner exception for details)", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.EntityType != null) ? this.EntityType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.EntityType != null) ? this.EntityType.MarkIfInternal() : string.Empty) : ((this.EntityType != null) ? this.EntityType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x00096C22 File Offset: 0x00094E22
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

		// Token: 0x060029D2 RID: 10706 RVA: 0x00096C40 File Offset: 0x00094E40
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EntityType={0}", new object[] { (this.EntityType != null) ? this.EntityType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EntityType={0}", new object[] { (this.EntityType != null) ? this.EntityType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "EntityType={0}", new object[] { (this.EntityType != null) ? this.EntityType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00096D31 File Offset: 0x00094F31
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x00096D3A File Offset: 0x00094F3A
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00096D43 File Offset: 0x00094F43
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00096D31 File Offset: 0x00094F31
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00096D4C File Offset: 0x00094F4C
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

		// Token: 0x04000EC3 RID: 3779
		private string creationMessage;

		// Token: 0x04000EC4 RID: 3780
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EC5 RID: 3781
		private Type m_entityType;
	}
}
