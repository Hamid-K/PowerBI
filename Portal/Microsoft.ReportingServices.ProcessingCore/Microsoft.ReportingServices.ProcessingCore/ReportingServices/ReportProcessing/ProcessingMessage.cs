using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000626 RID: 1574
	[Serializable]
	public sealed class ProcessingMessage : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060056B3 RID: 22195 RVA: 0x0016E23C File Offset: 0x0016C43C
		public ProcessingMessage(ProcessingErrorCode code, Severity severity, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, string message, ProcessingMessageList innerMessages, string diagnosticDetails)
		{
			this.m_code = code;
			this.m_commonCode = ErrorCode.rsProcessingError;
			this.m_severity = severity;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_propertyName = propertyName;
			this.m_message = message;
			this.m_processingMessages = innerMessages;
			this.m_diagnosticDetails = diagnosticDetails;
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x0016E294 File Offset: 0x0016C494
		public ProcessingMessage(ProcessingErrorCode code, Severity severity, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, string message, ProcessingMessageList innerMessages)
		{
			this.m_code = code;
			this.m_commonCode = ErrorCode.rsProcessingError;
			this.m_severity = severity;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_propertyName = propertyName;
			this.m_message = message;
			this.m_processingMessages = innerMessages;
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x0016E2E4 File Offset: 0x0016C4E4
		public ProcessingMessage(ErrorCode code, Severity severity, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, string message, ProcessingMessageList innerMessages)
		{
			this.m_code = ProcessingErrorCode.rsNone;
			this.m_commonCode = code;
			this.m_severity = severity;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_propertyName = propertyName;
			this.m_message = message;
			this.m_processingMessages = innerMessages;
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x0016E333 File Offset: 0x0016C533
		internal ProcessingMessage()
		{
		}

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x060056B7 RID: 22199 RVA: 0x0016E33B File Offset: 0x0016C53B
		// (set) Token: 0x060056B8 RID: 22200 RVA: 0x0016E343 File Offset: 0x0016C543
		public ProcessingErrorCode Code
		{
			get
			{
				return this.m_code;
			}
			set
			{
				this.m_code = value;
			}
		}

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x060056B9 RID: 22201 RVA: 0x0016E34C File Offset: 0x0016C54C
		// (set) Token: 0x060056BA RID: 22202 RVA: 0x0016E354 File Offset: 0x0016C554
		public ErrorCode CommonCode
		{
			get
			{
				return this.m_commonCode;
			}
			set
			{
				this.m_commonCode = value;
			}
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x060056BB RID: 22203 RVA: 0x0016E35D File Offset: 0x0016C55D
		// (set) Token: 0x060056BC RID: 22204 RVA: 0x0016E365 File Offset: 0x0016C565
		public Severity Severity
		{
			get
			{
				return this.m_severity;
			}
			set
			{
				this.m_severity = value;
			}
		}

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x060056BD RID: 22205 RVA: 0x0016E36E File Offset: 0x0016C56E
		// (set) Token: 0x060056BE RID: 22206 RVA: 0x0016E376 File Offset: 0x0016C576
		public Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
			set
			{
				this.m_objectType = value;
			}
		}

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x060056BF RID: 22207 RVA: 0x0016E37F File Offset: 0x0016C57F
		// (set) Token: 0x060056C0 RID: 22208 RVA: 0x0016E387 File Offset: 0x0016C587
		public string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
			set
			{
				this.m_objectName = value;
			}
		}

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x060056C1 RID: 22209 RVA: 0x0016E390 File Offset: 0x0016C590
		// (set) Token: 0x060056C2 RID: 22210 RVA: 0x0016E398 File Offset: 0x0016C598
		public string PropertyName
		{
			get
			{
				return this.m_propertyName;
			}
			set
			{
				this.m_propertyName = value;
			}
		}

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x0016E3A1 File Offset: 0x0016C5A1
		// (set) Token: 0x060056C4 RID: 22212 RVA: 0x0016E3A9 File Offset: 0x0016C5A9
		public string Message
		{
			get
			{
				return this.m_message;
			}
			set
			{
				this.m_message = value;
			}
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x060056C5 RID: 22213 RVA: 0x0016E3B2 File Offset: 0x0016C5B2
		// (set) Token: 0x060056C6 RID: 22214 RVA: 0x0016E3BA File Offset: 0x0016C5BA
		public ProcessingMessageList ProcessingMessages
		{
			get
			{
				return this.m_processingMessages;
			}
			set
			{
				this.m_processingMessages = value;
			}
		}

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x060056C7 RID: 22215 RVA: 0x0016E3C3 File Offset: 0x0016C5C3
		// (set) Token: 0x060056C8 RID: 22216 RVA: 0x0016E3CB File Offset: 0x0016C5CB
		public string DiagnosticDetails
		{
			get
			{
				return this.m_diagnosticDetails;
			}
			set
			{
				this.m_diagnosticDetails = value;
			}
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x0016E3D4 File Offset: 0x0016C5D4
		public string FormatMessage()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} ({1}.{2}) : {3} [{4}]", new object[]
			{
				(this.m_severity == Severity.Warning) ? "Warning" : "Error",
				this.m_objectName,
				this.m_propertyName,
				this.m_message,
				this.m_code
			});
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x0016E438 File Offset: 0x0016C638
		internal static Microsoft.ReportingServices.ReportProcessing.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportProcessing.Persistence.Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.None, new MemberInfoList
			{
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.Code, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.Severity, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.ObjectType, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.ObjectName, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.PropertyName, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.Message, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.ProcessingMessages, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ProcessingMessageList),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.CommonCode, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.DiagnosticDetails, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.String)
			});
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x0016E508 File Offset: 0x0016C708
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetNewDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ProcessingMessage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo>
			{
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Code, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Severity, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ObjectType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ObjectName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.PropertyName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Message, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ProcessingMessages, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ProcessingMessage),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.CommonCode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DiagnosticDetails, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.String)
			});
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x0016E5D8 File Offset: 0x0016C7D8
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ProcessingMessage.m_Declaration);
			while (writer.NextMember())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ProcessingMessages:
					writer.Write(this.m_processingMessages);
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Code:
					writer.WriteEnum((int)this.m_code);
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Severity:
					writer.WriteEnum((int)this.m_severity);
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ObjectType:
					writer.WriteEnum((int)this.m_objectType);
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ObjectName:
					writer.Write(this.m_objectName);
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.PropertyName:
					writer.Write(this.m_propertyName);
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Message:
					writer.Write(this.m_message);
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.CommonCode:
					writer.WriteEnum((int)this.m_commonCode);
					break;
				default:
					if (memberName != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DiagnosticDetails)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_diagnosticDetails);
					}
					break;
				}
			}
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x0016E6E0 File Offset: 0x0016C8E0
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ProcessingMessage.m_Declaration);
			while (reader.NextMember())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ProcessingMessages:
					this.m_processingMessages = reader.ReadListOfRIFObjects<ProcessingMessageList>();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Code:
					this.m_code = (ProcessingErrorCode)reader.ReadEnum();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Severity:
					this.m_severity = (Severity)reader.ReadEnum();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ObjectType:
					this.m_objectType = (Microsoft.ReportingServices.ReportProcessing.ObjectType)reader.ReadEnum();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ObjectName:
					this.m_objectName = reader.ReadString();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.PropertyName:
					this.m_propertyName = reader.ReadString();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Message:
					this.m_message = reader.ReadString();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.CommonCode:
					this.m_commonCode = (ErrorCode)reader.ReadEnum();
					break;
				default:
					if (memberName != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DiagnosticDetails)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_diagnosticDetails = reader.ReadString();
					}
					break;
				}
			}
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x0016E7E8 File Offset: 0x0016C9E8
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x0016E7F5 File Offset: 0x0016C9F5
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ProcessingMessage;
		}

		// Token: 0x04002DCD RID: 11725
		private ProcessingErrorCode m_code;

		// Token: 0x04002DCE RID: 11726
		private Severity m_severity;

		// Token: 0x04002DCF RID: 11727
		private Microsoft.ReportingServices.ReportProcessing.ObjectType m_objectType;

		// Token: 0x04002DD0 RID: 11728
		private string m_objectName;

		// Token: 0x04002DD1 RID: 11729
		private string m_propertyName;

		// Token: 0x04002DD2 RID: 11730
		private string m_message;

		// Token: 0x04002DD3 RID: 11731
		private ProcessingMessageList m_processingMessages;

		// Token: 0x04002DD4 RID: 11732
		private ErrorCode m_commonCode;

		// Token: 0x04002DD5 RID: 11733
		private string m_diagnosticDetails;

		// Token: 0x04002DD6 RID: 11734
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ProcessingMessage.GetNewDeclaration();
	}
}
