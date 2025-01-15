using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A0 RID: 160
	public struct ResultType
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x0001A144 File Offset: 0x00018344
		internal ResultType(DataType dataType, Cardinality cardinality, bool nullable)
		{
			this.m_dataType = dataType;
			this.m_cardinality = cardinality;
			this.m_nullable = nullable;
			this.m_entityKeyTarget = null;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001A162 File Offset: 0x00018362
		internal ResultType(IQueryEntity entityKeyTarget, bool nullable)
		{
			this = new ResultType(DataType.EntityKey, Cardinality.One, nullable);
			this.m_entityKeyTarget = entityKeyTarget;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001A174 File Offset: 0x00018374
		public static ResultType[] FromDataTypes(DataType[] dataTypes, Cardinality cardinality, bool nullable)
		{
			if (dataTypes == null)
			{
				throw new ArgumentNullException("dataTypes");
			}
			ResultType[] array = new ResultType[dataTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ResultType(dataTypes[i], cardinality, nullable);
			}
			return array;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0001A1B8 File Offset: 0x000183B8
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public DataType DataType
		{
			get
			{
				return this.m_dataType;
			}
			set
			{
				this.m_dataType = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001A1C9 File Offset: 0x000183C9
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0001A1D1 File Offset: 0x000183D1
		public Cardinality Cardinality
		{
			get
			{
				return this.m_cardinality;
			}
			set
			{
				this.m_cardinality = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001A1DA File Offset: 0x000183DA
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0001A1E2 File Offset: 0x000183E2
		public bool Nullable
		{
			get
			{
				return this.m_nullable;
			}
			set
			{
				this.m_nullable = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001A1EB File Offset: 0x000183EB
		public IQueryEntity EntityKeyTarget
		{
			get
			{
				return this.m_entityKeyTarget;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001A1F4 File Offset: 0x000183F4
		internal void SetEntityKeyTarget(IQueryEntity entityKeyTarget)
		{
			if (entityKeyTarget == null)
			{
				throw new InternalModelingException("entityKeyTarget is null");
			}
			if (this.m_dataType != DataType.EntityKey && this.m_dataType != DataType.Null)
			{
				throw new InternalModelingException("SetEntityKeyTarget is called on a non-entitykey/non-null ResultType.");
			}
			if (this.m_entityKeyTarget == null)
			{
				this.m_entityKeyTarget = entityKeyTarget;
				return;
			}
			throw new InternalModelingException("Attempt to reassign m_entityKeyTarget.");
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001A246 File Offset: 0x00018446
		public override bool Equals(object obj)
		{
			return obj is ResultType && this == (ResultType)obj;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001A263 File Offset: 0x00018463
		public override int GetHashCode()
		{
			return this.m_dataType.GetHashCode();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001A278 File Offset: 0x00018478
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{{{0}, Cardinality={1}, Nullable={2}{3}}}", new object[]
			{
				this.m_dataType,
				this.m_cardinality,
				this.m_nullable,
				(this.m_entityKeyTarget != null) ? (", Entity=" + this.m_entityKeyTarget.Name) : null
			});
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001A2E4 File Offset: 0x000184E4
		public static bool operator ==(ResultType x, ResultType y)
		{
			return x.DataType == y.DataType && x.Cardinality == y.Cardinality && x.Nullable == y.Nullable && x.EntityKeyTarget == y.EntityKeyTarget;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001A333 File Offset: 0x00018533
		public static bool operator !=(ResultType x, ResultType y)
		{
			return !(x == y);
		}

		// Token: 0x040003AC RID: 940
		private DataType m_dataType;

		// Token: 0x040003AD RID: 941
		private Cardinality m_cardinality;

		// Token: 0x040003AE RID: 942
		private bool m_nullable;

		// Token: 0x040003AF RID: 943
		private IQueryEntity m_entityKeyTarget;
	}
}
