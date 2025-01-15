using System;
using System.Globalization;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200051A RID: 1306
	[DataContract]
	[CannotApplyEqualityOperator]
	[Serializable]
	public class ElementId : IEquatable<ElementId>
	{
		// Token: 0x06002873 RID: 10355 RVA: 0x00091F5F File Offset: 0x0009015F
		public ElementId([NotNull] string name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(name, "name");
			this.m_name = name;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x00091F79 File Offset: 0x00090179
		public bool Equals(ElementId other)
		{
			return other != null && this.m_name.Equals(other.m_name, StringComparison.Ordinal);
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x00091F92 File Offset: 0x00090192
		public override int GetHashCode()
		{
			return this.m_name.GetHashCode();
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x00091F9F File Offset: 0x0009019F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ElementId);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00091FAD File Offset: 0x000901AD
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x00091FB5 File Offset: 0x000901B5
		public static ElementId Any
		{
			get
			{
				return ElementId.sm_any;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x00091FBC File Offset: 0x000901BC
		public static ElementId None
		{
			get
			{
				return ElementId.sm_none;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x0000E609 File Offset: 0x0000C809
		public string Name
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x00091FC3 File Offset: 0x000901C3
		private static string ConcatenateFullyQualifiedNames(string first, string second)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}-{1}", new object[] { first, second });
		}

		// Token: 0x04000E03 RID: 3587
		[DataMember]
		private readonly string m_name;

		// Token: 0x04000E04 RID: 3588
		private static readonly ElementId sm_any = new ElementId("ANY_206e5fe5-713b-470c-8ab4-bd98dc5de63a");

		// Token: 0x04000E05 RID: 3589
		private static readonly ElementId sm_none = new ElementId("NONE_206e5fe5-713b-470c-8ab4-bd98dc5de63a");
	}
}
