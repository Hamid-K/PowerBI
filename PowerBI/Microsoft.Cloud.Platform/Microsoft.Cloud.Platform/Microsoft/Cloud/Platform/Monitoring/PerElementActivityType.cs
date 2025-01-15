using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000085 RID: 133
	[CannotApplyEqualityOperator]
	public class PerElementActivityType : IEquatable<PerElementActivityType>
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000E611 File Offset: 0x0000C811
		public ElementId ElementId
		{
			get
			{
				return this.m_elementId;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000E619 File Offset: 0x0000C819
		public ActivityType FlowName
		{
			get
			{
				return this.m_flowName;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000E621 File Offset: 0x0000C821
		public static ElementId DefaultElementId
		{
			get
			{
				return PerElementActivityType.c_defaultElementActivityType.ElementId;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000E62D File Offset: 0x0000C82D
		public static ActivityType DefaultActivityType
		{
			get
			{
				return PerElementActivityType.c_defaultElementActivityType.FlowName;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000E639 File Offset: 0x0000C839
		public static PerElementActivityType Default
		{
			get
			{
				return PerElementActivityType.c_defaultElementActivityType;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E640 File Offset: 0x0000C840
		public PerElementActivityType(ElementId elementId, ActivityType flowName)
		{
			this.m_elementId = elementId;
			this.m_flowName = flowName;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000E656 File Offset: 0x0000C856
		public bool Equals(PerElementActivityType other)
		{
			return other != null && this.m_elementId.Equals(other.m_elementId) && this.m_flowName.Equals(other.m_flowName);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000E681 File Offset: 0x0000C881
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PerElementActivityType);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E68F File Offset: 0x0000C88F
		public override int GetHashCode()
		{
			return this.m_elementId.GetHashCode() ^ this.m_flowName.GetHashCode();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "ElementId={0} Activity={1}", new object[] { this.m_elementId, this.m_flowName });
		}

		// Token: 0x04000151 RID: 337
		private readonly ElementId m_elementId;

		// Token: 0x04000152 RID: 338
		private readonly ActivityType m_flowName;

		// Token: 0x04000153 RID: 339
		private static readonly string c_default = "DFLT";

		// Token: 0x04000154 RID: 340
		private static readonly PerElementActivityType c_defaultElementActivityType = new PerElementActivityType(new ElementId(PerElementActivityType.c_default), new ActivityType(PerElementActivityType.c_default));
	}
}
