using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000A5 RID: 165
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CompositeApplicationSwitchesException : ApplicationSwitchesException
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00010C94 File Offset: 0x0000EE94
		private string ConcatExceptionsMessages()
		{
			string text = base.Message;
			foreach (ApplicationSwitchesException ex in this.Exceptions)
			{
				text = string.Concat(new string[]
				{
					text,
					Environment.NewLine,
					"  ",
					ex.Message,
					" (",
					ex.GetType().ToString(),
					")"
				});
			}
			return text;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00010D2C File Offset: 0x0000EF2C
		public override string ToString()
		{
			return this.ConcatExceptionsMessages() + Environment.NewLine + base.ToString();
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00010D44 File Offset: 0x0000EF44
		public override string Message
		{
			get
			{
				return this.ConcatExceptionsMessages();
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00010D4C File Offset: 0x0000EF4C
		internal void Add(ApplicationSwitchesException ex)
		{
			this.Exceptions.Add(ex);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00010D5A File Offset: 0x0000EF5A
		internal int Count
		{
			get
			{
				return this.Exceptions.Count;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00010D67 File Offset: 0x0000EF67
		internal bool IsEmpty()
		{
			return this.Count == 0;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00010D72 File Offset: 0x0000EF72
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00010D7A File Offset: 0x0000EF7A
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x00010D82 File Offset: 0x0000EF82
		public IList<ApplicationSwitchesException> Exceptions
		{
			get
			{
				return this.m_exceptions;
			}
			protected set
			{
				this.m_exceptions = value;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00010D8B File Offset: 0x0000EF8B
		public CompositeApplicationSwitchesException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<IList<ApplicationSwitchesException>>();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00010DAA File Offset: 0x0000EFAA
		public CompositeApplicationSwitchesException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00010DCC File Offset: 0x0000EFCC
		public CompositeApplicationSwitchesException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00010DF4 File Offset: 0x0000EFF4
		protected CompositeApplicationSwitchesException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CompositeApplicationSwitchesException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Exceptions = (IList<ApplicationSwitchesException>)info.GetValue("CompositeApplicationSwitchesException_Exceptions", typeof(IList<ApplicationSwitchesException>));
			}
			catch (SerializationException)
			{
				this.Exceptions = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CompositeApplicationSwitchesException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public CompositeApplicationSwitchesException(IList<ApplicationSwitchesException> exceptions)
		{
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00010EF5 File Offset: 0x0000F0F5
		public CompositeApplicationSwitchesException(IList<ApplicationSwitchesException> exceptions, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00010F1E File Offset: 0x0000F11E
		public CompositeApplicationSwitchesException(IList<ApplicationSwitchesException> exceptions, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00010F50 File Offset: 0x0000F150
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00010F87 File Offset: 0x0000F187
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00010F90 File Offset: 0x0000F190
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CompositeApplicationSwitchesException))
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

		// Token: 0x060004AB RID: 1195 RVA: 0x00011060 File Offset: 0x0000F260
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CompositeApplicationSwitchesException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CompositeApplicationSwitchesException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Exceptions != null)
			{
				info.AddValue("CompositeApplicationSwitchesException_Exceptions", this.Exceptions, typeof(IList<ApplicationSwitchesException>));
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000110DE File Offset: 0x0000F2DE
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The application switches handed to this application failed to validate:", new object[0]);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000110F8 File Offset: 0x0000F2F8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? this.Exceptions.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? this.Exceptions.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? this.Exceptions.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x04000197 RID: 407
		private string creationMessage;

		// Token: 0x04000198 RID: 408
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000199 RID: 409
		private IList<ApplicationSwitchesException> m_exceptions = new List<ApplicationSwitchesException>();
	}
}
