using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000C9 RID: 201
	[DebuggerDisplay("State = {state}")]
	public sealed class LinkDescriptor : Descriptor
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0001C0BF File Offset: 0x0001A2BF
		internal LinkDescriptor(object source, string sourceProperty, object target, ClientEdmModel model)
			: this(source, sourceProperty, target, EntityStates.Unchanged)
		{
			this.IsSourcePropertyCollection = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(source.GetType())).GetProperty(sourceProperty, UndeclaredPropertyBehavior.ThrowException).IsEntityCollection;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001C0F1 File Offset: 0x0001A2F1
		internal LinkDescriptor(object source, string sourceProperty, object target, EntityStates state)
			: base(state)
		{
			this.source = source;
			this.sourceProperty = sourceProperty;
			this.target = target;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001C110 File Offset: 0x0001A310
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001C118 File Offset: 0x0001A318
		[SuppressMessage("Microsoft.Performance", "CA1811", Justification = "The setter is called during de-serialization")]
		public object Target
		{
			get
			{
				return this.target;
			}
			internal set
			{
				this.target = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001C121 File Offset: 0x0001A321
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001C129 File Offset: 0x0001A329
		[SuppressMessage("Microsoft.Performance", "CA1811", Justification = "The setter is called during de-serialization")]
		public object Source
		{
			get
			{
				return this.source;
			}
			internal set
			{
				this.source = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001C132 File Offset: 0x0001A332
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001C13A File Offset: 0x0001A33A
		[SuppressMessage("Microsoft.Performance", "CA1811", Justification = "The setter is called during de-serialization")]
		public string SourceProperty
		{
			get
			{
				return this.sourceProperty;
			}
			internal set
			{
				this.sourceProperty = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00004A70 File Offset: 0x00002C70
		internal override DescriptorKind DescriptorKind
		{
			get
			{
				return DescriptorKind.Link;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001C143 File Offset: 0x0001A343
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0001C14B File Offset: 0x0001A34B
		internal bool IsSourcePropertyCollection { get; set; }

		// Token: 0x06000690 RID: 1680 RVA: 0x0000B028 File Offset: 0x00009228
		internal override void ClearChanges()
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001C154 File Offset: 0x0001A354
		internal bool IsEquivalent(object src, string srcPropName, object targ)
		{
			return this.source == src && this.target == targ && this.sourceProperty == srcPropName;
		}

		// Token: 0x040002E5 RID: 741
		internal static readonly IEqualityComparer<LinkDescriptor> EquivalenceComparer = new LinkDescriptor.Equivalent();

		// Token: 0x040002E6 RID: 742
		private object source;

		// Token: 0x040002E7 RID: 743
		private string sourceProperty;

		// Token: 0x040002E8 RID: 744
		private object target;

		// Token: 0x020001A1 RID: 417
		private sealed class Equivalent : IEqualityComparer<LinkDescriptor>
		{
			// Token: 0x06000E9E RID: 3742 RVA: 0x00031872 File Offset: 0x0002FA72
			public bool Equals(LinkDescriptor x, LinkDescriptor y)
			{
				return x != null && y != null && x.IsEquivalent(y.source, y.sourceProperty, y.target);
			}

			// Token: 0x06000E9F RID: 3743 RVA: 0x00031894 File Offset: 0x0002FA94
			public int GetHashCode(LinkDescriptor obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.Source.GetHashCode() ^ ((obj.Target != null) ? obj.Target.GetHashCode() : 0) ^ obj.SourceProperty.GetHashCode();
			}
		}
	}
}
