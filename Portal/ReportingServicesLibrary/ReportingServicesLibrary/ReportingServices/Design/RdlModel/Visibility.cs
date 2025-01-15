using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000429 RID: 1065
	[TypeConverter(typeof(Visibility.VisibilityConverter))]
	public sealed class Visibility : IVoluntarySerializable
	{
		// Token: 0x060021F0 RID: 8688 RVA: 0x00081A18 File Offset: 0x0007FC18
		public Visibility()
		{
			this.m_hidden = new TrueFalseString("false");
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00081A3B File Offset: 0x0007FC3B
		internal Visibility(Visibility value)
			: this(new TrueFalseString(value.Hidden.Value), value.ToggleItem)
		{
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x00081A59 File Offset: 0x0007FC59
		public Visibility(TrueFalseString hidden, string toggleItem)
		{
			this.m_hidden = hidden;
			if (toggleItem != null && toggleItem.Length > 0)
			{
				this.m_toggleItem = toggleItem;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x00081A86 File Offset: 0x0007FC86
		// (set) Token: 0x060021F4 RID: 8692 RVA: 0x00081A8E File Offset: 0x0007FC8E
		[Editor("ExpressionEditor", typeof(UITypeEditor))]
		[DefaultValue(typeof(TrueFalseString), "")]
		[SRDescription("Hidden")]
		public TrueFalseString Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x00081A97 File Offset: 0x0007FC97
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x00081A9F File Offset: 0x0007FC9F
		[DefaultValue("")]
		[TypeConverter("ToggleConverter")]
		[SRDescription("ToggleItem")]
		public string ToggleItem
		{
			get
			{
				return this.m_toggleItem;
			}
			set
			{
				this.m_toggleItem = value;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x00081AA8 File Offset: 0x0007FCA8
		// (set) Token: 0x060021F8 RID: 8696 RVA: 0x00081AB0 File Offset: 0x0007FCB0
		internal object Owner
		{
			get
			{
				return this.m_owner;
			}
			set
			{
				this.m_owner = value;
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00081ABC File Offset: 0x0007FCBC
		public override bool Equals(object obj)
		{
			if (!(obj is Visibility))
			{
				return false;
			}
			Visibility visibility = (Visibility)obj;
			return this.Hidden.Equals(visibility.Hidden) && ((string.IsNullOrEmpty(this.ToggleItem) && string.IsNullOrEmpty(visibility.ToggleItem)) || this.ToggleItem == visibility.ToggleItem);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00081B1C File Offset: 0x0007FD1C
		public override int GetHashCode()
		{
			return ((this.Hidden != null) ? this.Hidden.GetHashCode() : 0) ^ ((this.ToggleItem != null) ? this.ToggleItem.GetHashCode() : 0);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00081B4B File Offset: 0x0007FD4B
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return !this.Equals(Visibility.m_empty);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x04000EE4 RID: 3812
		private TrueFalseString m_hidden;

		// Token: 0x04000EE5 RID: 3813
		private string m_toggleItem = "";

		// Token: 0x04000EE6 RID: 3814
		private object m_owner;

		// Token: 0x04000EE7 RID: 3815
		private static readonly Visibility m_empty = new Visibility();

		// Token: 0x02000528 RID: 1320
		internal sealed class VisibilityConverter : TypeConverter
		{
			// Token: 0x06002526 RID: 9510 RVA: 0x00087BB1 File Offset: 0x00085DB1
			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				return new Visibility((TrueFalseString)propertyValues["Hidden"], (string)propertyValues["ToggleItem"]);
			}

			// Token: 0x06002527 RID: 9511 RVA: 0x000053DC File Offset: 0x000035DC
			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06002528 RID: 9512 RVA: 0x00087BD8 File Offset: 0x00085DD8
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				return TypeDescriptor.GetProperties(typeof(Visibility), attributes).Sort(new string[] { "Hidden", "ToggleItem" });
			}

			// Token: 0x06002529 RID: 9513 RVA: 0x000053DC File Offset: 0x000035DC
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}
	}
}
