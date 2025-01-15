using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200078C RID: 1932
	public sealed class ObjectModelImpl : ObjectModel, IConvertible
	{
		// Token: 0x06006BAA RID: 27562 RVA: 0x001B5F44 File Offset: 0x001B4144
		internal ObjectModelImpl(ReportProcessing.ProcessingContext processingContext)
		{
			this.m_fields = null;
			this.m_parameters = null;
			this.m_globals = null;
			this.m_user = null;
			this.m_reportItems = null;
			this.m_aggregates = null;
			this.m_dataSets = null;
			this.m_dataSources = null;
			this.m_processingContext = processingContext;
		}

		// Token: 0x06006BAB RID: 27563 RVA: 0x001B5F98 File Offset: 0x001B4198
		internal ObjectModelImpl(ObjectModelImpl copy, ReportProcessing.ProcessingContext processingContext)
		{
			this.m_fields = null;
			this.m_parameters = copy.m_parameters;
			this.m_globals = copy.m_globals;
			this.m_user = copy.m_user;
			this.m_reportItems = copy.m_reportItems;
			this.m_aggregates = copy.m_aggregates;
			this.m_dataSets = copy.m_dataSets;
			this.m_dataSources = copy.m_dataSources;
			this.m_processingContext = processingContext;
		}

		// Token: 0x17002566 RID: 9574
		// (get) Token: 0x06006BAC RID: 27564 RVA: 0x001B600D File Offset: 0x001B420D
		public override Fields Fields
		{
			get
			{
				return this.FieldsImpl;
			}
		}

		// Token: 0x17002567 RID: 9575
		// (get) Token: 0x06006BAD RID: 27565 RVA: 0x001B6015 File Offset: 0x001B4215
		public override Parameters Parameters
		{
			get
			{
				return this.ParametersImpl;
			}
		}

		// Token: 0x17002568 RID: 9576
		// (get) Token: 0x06006BAE RID: 27566 RVA: 0x001B601D File Offset: 0x001B421D
		public override Globals Globals
		{
			get
			{
				return this.GlobalsImpl;
			}
		}

		// Token: 0x17002569 RID: 9577
		// (get) Token: 0x06006BAF RID: 27567 RVA: 0x001B6025 File Offset: 0x001B4225
		public override User User
		{
			get
			{
				return this.UserImpl;
			}
		}

		// Token: 0x1700256A RID: 9578
		// (get) Token: 0x06006BB0 RID: 27568 RVA: 0x001B602D File Offset: 0x001B422D
		public override ReportItems ReportItems
		{
			get
			{
				return this.ReportItemsImpl;
			}
		}

		// Token: 0x1700256B RID: 9579
		// (get) Token: 0x06006BB1 RID: 27569 RVA: 0x001B6035 File Offset: 0x001B4235
		public override Aggregates Aggregates
		{
			get
			{
				return this.AggregatesImpl;
			}
		}

		// Token: 0x1700256C RID: 9580
		// (get) Token: 0x06006BB2 RID: 27570 RVA: 0x001B603D File Offset: 0x001B423D
		public override DataSets DataSets
		{
			get
			{
				return this.DataSetsImpl;
			}
		}

		// Token: 0x1700256D RID: 9581
		// (get) Token: 0x06006BB3 RID: 27571 RVA: 0x001B6045 File Offset: 0x001B4245
		public override DataSources DataSources
		{
			get
			{
				return this.DataSourcesImpl;
			}
		}

		// Token: 0x1700256E RID: 9582
		// (get) Token: 0x06006BB4 RID: 27572 RVA: 0x001B604D File Offset: 0x001B424D
		// (set) Token: 0x06006BB5 RID: 27573 RVA: 0x001B6055 File Offset: 0x001B4255
		internal FieldsImpl FieldsImpl
		{
			get
			{
				return this.m_fields;
			}
			set
			{
				this.m_fields = value;
			}
		}

		// Token: 0x1700256F RID: 9583
		// (get) Token: 0x06006BB6 RID: 27574 RVA: 0x001B605E File Offset: 0x001B425E
		// (set) Token: 0x06006BB7 RID: 27575 RVA: 0x001B6066 File Offset: 0x001B4266
		internal ParametersImpl ParametersImpl
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x17002570 RID: 9584
		// (get) Token: 0x06006BB8 RID: 27576 RVA: 0x001B606F File Offset: 0x001B426F
		// (set) Token: 0x06006BB9 RID: 27577 RVA: 0x001B6077 File Offset: 0x001B4277
		internal GlobalsImpl GlobalsImpl
		{
			get
			{
				return this.m_globals;
			}
			set
			{
				this.m_globals = value;
			}
		}

		// Token: 0x17002571 RID: 9585
		// (get) Token: 0x06006BBA RID: 27578 RVA: 0x001B6080 File Offset: 0x001B4280
		// (set) Token: 0x06006BBB RID: 27579 RVA: 0x001B6088 File Offset: 0x001B4288
		internal UserImpl UserImpl
		{
			get
			{
				return this.m_user;
			}
			set
			{
				this.m_user = value;
			}
		}

		// Token: 0x17002572 RID: 9586
		// (get) Token: 0x06006BBC RID: 27580 RVA: 0x001B6091 File Offset: 0x001B4291
		// (set) Token: 0x06006BBD RID: 27581 RVA: 0x001B6099 File Offset: 0x001B4299
		internal ReportItemsImpl ReportItemsImpl
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x17002573 RID: 9587
		// (get) Token: 0x06006BBE RID: 27582 RVA: 0x001B60A2 File Offset: 0x001B42A2
		// (set) Token: 0x06006BBF RID: 27583 RVA: 0x001B60AA File Offset: 0x001B42AA
		internal AggregatesImpl AggregatesImpl
		{
			get
			{
				return this.m_aggregates;
			}
			set
			{
				this.m_aggregates = value;
			}
		}

		// Token: 0x17002574 RID: 9588
		// (get) Token: 0x06006BC0 RID: 27584 RVA: 0x001B60B3 File Offset: 0x001B42B3
		// (set) Token: 0x06006BC1 RID: 27585 RVA: 0x001B60BB File Offset: 0x001B42BB
		internal DataSetsImpl DataSetsImpl
		{
			get
			{
				return this.m_dataSets;
			}
			set
			{
				this.m_dataSets = value;
			}
		}

		// Token: 0x17002575 RID: 9589
		// (get) Token: 0x06006BC2 RID: 27586 RVA: 0x001B60C4 File Offset: 0x001B42C4
		// (set) Token: 0x06006BC3 RID: 27587 RVA: 0x001B60CC File Offset: 0x001B42CC
		internal DataSourcesImpl DataSourcesImpl
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x06006BC4 RID: 27588 RVA: 0x001B60D5 File Offset: 0x001B42D5
		public override bool InScope(string scope)
		{
			return this.m_processingContext.ReportRuntime.InScope(scope);
		}

		// Token: 0x06006BC5 RID: 27589 RVA: 0x001B60E8 File Offset: 0x001B42E8
		public override int RecursiveLevel(string scope)
		{
			return this.m_processingContext.ReportRuntime.RecursiveLevel(scope);
		}

		// Token: 0x06006BC6 RID: 27590 RVA: 0x001B60FB File Offset: 0x001B42FB
		public override string CreateDrillthroughContext()
		{
			return this.m_processingContext.ReportRuntime.CreateDrillthroughContext();
		}

		// Token: 0x06006BC7 RID: 27591 RVA: 0x001B610D File Offset: 0x001B430D
		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Object;
		}

		// Token: 0x06006BC8 RID: 27592 RVA: 0x001B6110 File Offset: 0x001B4310
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BC9 RID: 27593 RVA: 0x001B6117 File Offset: 0x001B4317
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BCA RID: 27594 RVA: 0x001B611E File Offset: 0x001B431E
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BCB RID: 27595 RVA: 0x001B6125 File Offset: 0x001B4325
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BCC RID: 27596 RVA: 0x001B612C File Offset: 0x001B432C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BCD RID: 27597 RVA: 0x001B6133 File Offset: 0x001B4333
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BCE RID: 27598 RVA: 0x001B613A File Offset: 0x001B433A
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BCF RID: 27599 RVA: 0x001B6141 File Offset: 0x001B4341
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BD0 RID: 27600 RVA: 0x001B6148 File Offset: 0x001B4348
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BD1 RID: 27601 RVA: 0x001B614F File Offset: 0x001B434F
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BD2 RID: 27602 RVA: 0x001B6156 File Offset: 0x001B4356
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BD3 RID: 27603 RVA: 0x001B615D File Offset: 0x001B435D
		string IConvertible.ToString(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BD4 RID: 27604 RVA: 0x001B6164 File Offset: 0x001B4364
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			if (conversionType == typeof(ObjectModel))
			{
				return this;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006BD5 RID: 27605 RVA: 0x001B617F File Offset: 0x001B437F
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BD6 RID: 27606 RVA: 0x001B6186 File Offset: 0x001B4386
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006BD7 RID: 27607 RVA: 0x001B618D File Offset: 0x001B438D
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400361F RID: 13855
		private FieldsImpl m_fields;

		// Token: 0x04003620 RID: 13856
		private ParametersImpl m_parameters;

		// Token: 0x04003621 RID: 13857
		private GlobalsImpl m_globals;

		// Token: 0x04003622 RID: 13858
		private UserImpl m_user;

		// Token: 0x04003623 RID: 13859
		private ReportItemsImpl m_reportItems;

		// Token: 0x04003624 RID: 13860
		private AggregatesImpl m_aggregates;

		// Token: 0x04003625 RID: 13861
		private DataSetsImpl m_dataSets;

		// Token: 0x04003626 RID: 13862
		private DataSourcesImpl m_dataSources;

		// Token: 0x04003627 RID: 13863
		private ReportProcessing.ProcessingContext m_processingContext;

		// Token: 0x04003628 RID: 13864
		internal const string NamespacePrefix = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.";
	}
}
