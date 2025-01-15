using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	public class ArgumentConstraintException : ArgumentException
	{
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00023D53 File Offset: 0x00021F53
		public object Component
		{
			get
			{
				return this.m_component;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00023D5B File Offset: 0x00021F5B
		public string Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00023D63 File Offset: 0x00021F63
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00023D6B File Offset: 0x00021F6B
		public object Constraint
		{
			get
			{
				return this.m_constraint;
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00023D73 File Offset: 0x00021F73
		public ArgumentConstraintException(object component, string property, object value, object constraint, string message)
			: base(message, property)
		{
			this.m_component = component;
			this.m_property = property;
			this.m_value = value;
			this.m_constraint = constraint;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00023D9B File Offset: 0x00021F9B
		protected ArgumentConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000547 RID: 1351
		private readonly object m_component;

		// Token: 0x04000548 RID: 1352
		private readonly string m_property;

		// Token: 0x04000549 RID: 1353
		private readonly object m_value;

		// Token: 0x0400054A RID: 1354
		private readonly object m_constraint;
	}
}
