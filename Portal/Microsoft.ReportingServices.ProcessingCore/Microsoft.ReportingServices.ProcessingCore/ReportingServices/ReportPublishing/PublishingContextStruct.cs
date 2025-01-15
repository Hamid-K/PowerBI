using System;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200039B RID: 923
	internal struct PublishingContextStruct
	{
		// Token: 0x060025A1 RID: 9633 RVA: 0x000B451E File Offset: 0x000B271E
		internal PublishingContextStruct(LocationFlags location, ObjectType objectType, int maxExpressionLength, PublishingErrorContext errorContext)
		{
			this.m_location = location;
			this.m_objectType = objectType;
			this.m_objectName = null;
			this.m_prefixPropertyName = null;
			this.m_maxExpressionLength = maxExpressionLength;
			this.m_errorContext = errorContext;
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000B454B File Offset: 0x000B274B
		// (set) Token: 0x060025A3 RID: 9635 RVA: 0x000B4553 File Offset: 0x000B2753
		internal LocationFlags Location
		{
			get
			{
				return this.m_location;
			}
			set
			{
				this.m_location = value;
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x000B455C File Offset: 0x000B275C
		// (set) Token: 0x060025A5 RID: 9637 RVA: 0x000B4564 File Offset: 0x000B2764
		internal ObjectType ObjectType
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

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x000B456D File Offset: 0x000B276D
		// (set) Token: 0x060025A7 RID: 9639 RVA: 0x000B4575 File Offset: 0x000B2775
		internal string ObjectName
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

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000B457E File Offset: 0x000B277E
		// (set) Token: 0x060025A9 RID: 9641 RVA: 0x000B4586 File Offset: 0x000B2786
		internal string PrefixPropertyName
		{
			get
			{
				return this.m_prefixPropertyName;
			}
			set
			{
				this.m_prefixPropertyName = value;
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x000B458F File Offset: 0x000B278F
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x000B4597 File Offset: 0x000B2797
		internal PublishingErrorContext ErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
			set
			{
				this.m_errorContext = value;
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000B45A0 File Offset: 0x000B27A0
		internal Microsoft.ReportingServices.RdlExpressions.ExpressionParser.ExpressionContext CreateExpressionContext(Microsoft.ReportingServices.RdlExpressions.ExpressionParser.ExpressionType expressionType, DataType constantType, string propertyName, string dataSetName, PublishingContextBase publishingContext)
		{
			string text;
			if (string.IsNullOrEmpty(propertyName))
			{
				text = this.m_prefixPropertyName;
			}
			else if (string.IsNullOrEmpty(this.m_prefixPropertyName))
			{
				text = propertyName;
			}
			else
			{
				text = this.m_prefixPropertyName + propertyName;
			}
			return new Microsoft.ReportingServices.RdlExpressions.ExpressionParser.ExpressionContext(expressionType, constantType, this.m_location, this.m_objectType, this.m_objectName, text, dataSetName, this.m_maxExpressionLength, publishingContext);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000B4600 File Offset: 0x000B2800
		internal double ValidateSize(string size, string propertyName, ErrorContext errorContext)
		{
			double num;
			string text;
			PublishingValidator.ValidateSize(size, this.m_objectType, this.m_objectName, propertyName, true, errorContext, out num, out text);
			return num;
		}

		// Token: 0x040015F8 RID: 5624
		private LocationFlags m_location;

		// Token: 0x040015F9 RID: 5625
		private ObjectType m_objectType;

		// Token: 0x040015FA RID: 5626
		private string m_objectName;

		// Token: 0x040015FB RID: 5627
		private string m_prefixPropertyName;

		// Token: 0x040015FC RID: 5628
		private int m_maxExpressionLength;

		// Token: 0x040015FD RID: 5629
		private PublishingErrorContext m_errorContext;
	}
}
