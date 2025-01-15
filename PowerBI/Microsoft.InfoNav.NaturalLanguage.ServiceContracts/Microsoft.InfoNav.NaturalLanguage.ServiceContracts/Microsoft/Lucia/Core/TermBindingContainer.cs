using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;
using YamlDotNet.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000110 RID: 272
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class TermBindingContainer
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x0000A466 File Offset: 0x00008666
		public TermBindingContainer()
		{
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000A46E File Offset: 0x0000866E
		public TermBindingContainer(TermBinding binding)
		{
			Contract.CheckValue<TermBinding>(binding, "binding");
			this._binding = binding;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000A488 File Offset: 0x00008688
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0000A490 File Offset: 0x00008690
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public TableTermBinding Table
		{
			get
			{
				return this.GetBinding<TableTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000A499 File Offset: 0x00008699
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0000A4A1 File Offset: 0x000086A1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public PodTermBinding Pod
		{
			get
			{
				return this.GetBinding<PodTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000A4AA File Offset: 0x000086AA
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0000A4B2 File Offset: 0x000086B2
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public PropertyTermBinding Property
		{
			get
			{
				return this.GetBinding<PropertyTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000A4BB File Offset: 0x000086BB
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0000A4C3 File Offset: 0x000086C3
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public ValueTermBinding Value
		{
			get
			{
				return this.GetBinding<ValueTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000A4CC File Offset: 0x000086CC
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0000A4D4 File Offset: 0x000086D4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public RangeTermBinding Range
		{
			get
			{
				return this.GetBinding<RangeTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000A4DD File Offset: 0x000086DD
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0000A4E5 File Offset: 0x000086E5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public VisualizationTypeTermBinding VisType
		{
			get
			{
				return this.GetBinding<VisualizationTypeTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000A4EE File Offset: 0x000086EE
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000A4F6 File Offset: 0x000086F6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public CompositeTermBinding Composite
		{
			get
			{
				return this.GetBinding<CompositeTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000A4FF File Offset: 0x000086FF
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0000A507 File Offset: 0x00008707
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public CoreTermBinding Core
		{
			get
			{
				return this.GetBinding<CoreTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000A510 File Offset: 0x00008710
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0000A518 File Offset: 0x00008718
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public LiteralTermBinding Literal
		{
			get
			{
				return this.GetBinding<LiteralTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000A521 File Offset: 0x00008721
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0000A529 File Offset: 0x00008729
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public PhrasingTermBinding Phrasing
		{
			get
			{
				return this.GetBinding<PhrasingTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000A532 File Offset: 0x00008732
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000A53A File Offset: 0x0000873A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 110)]
		public InferredTermBinding InferredTerm
		{
			get
			{
				return this.GetBinding<InferredTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000A543 File Offset: 0x00008743
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000A54B File Offset: 0x0000874B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 120)]
		public TextualEntityTermBinding TextualEntity
		{
			get
			{
				return this.GetBinding<TextualEntityTermBinding>();
			}
			set
			{
				this.SetBinding(value);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000A554 File Offset: 0x00008754
		[YamlIgnore]
		public TermBinding Binding
		{
			get
			{
				return this._binding;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000A55C File Offset: 0x0000875C
		public static implicit operator TermBindingContainer(TermBinding value)
		{
			if (value != null)
			{
				return new TermBindingContainer(value);
			}
			return null;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000A569 File Offset: 0x00008769
		public override string ToString()
		{
			if (this._binding != null)
			{
				return this._binding.ToString();
			}
			return null;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000A580 File Offset: 0x00008780
		private TTermBinding GetBinding<TTermBinding>() where TTermBinding : TermBinding
		{
			return this._binding as TTermBinding;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000A592 File Offset: 0x00008792
		private void SetBinding(TermBinding binding)
		{
			Contract.CheckParam(this._binding == null || this._binding == binding, "binding", "An term binding container can only have a single value");
			this._binding = binding;
		}

		// Token: 0x040005C2 RID: 1474
		private TermBinding _binding;
	}
}
