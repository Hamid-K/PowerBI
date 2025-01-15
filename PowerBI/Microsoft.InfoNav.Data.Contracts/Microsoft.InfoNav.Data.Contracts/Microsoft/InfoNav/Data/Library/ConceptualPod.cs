using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000076 RID: 118
	[DebuggerDisplay("{EntityContainerName}.{Name}")]
	[ImmutableObject(true)]
	public sealed class ConceptualPod : IConceptualPod, IConceptualEntity, IConceptualDisplayItem, IEquatable<IConceptualEntity>, IBuiltConceptualType
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00007362 File Offset: 0x00005562
		private ConceptualPod(string name, string edmName, string displayName, string description, string edmEntityContainerName, ConceptualEntityVisibilityType visibility, bool cortanaEnabled)
		{
			this.Name = name;
			this.EdmName = edmName;
			this.DisplayName = displayName;
			this.Description = description;
			this.EntityContainerName = edmEntityContainerName;
			this.Visibility = visibility;
			this.CortanaEnabled = cortanaEnabled;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000739F File Offset: 0x0000559F
		public static ConceptualPod.Builder Create(string name, string edmName = null, string displayName = null, string description = null, string edmEntityContainerName = null, ConceptualEntityVisibilityType visibility = ConceptualEntityVisibilityType.AlwaysVisible, bool cortanaEnabled = false)
		{
			if (edmName == null)
			{
				edmName = name;
			}
			if (displayName == null)
			{
				displayName = name;
			}
			if (edmEntityContainerName == null)
			{
				edmEntityContainerName = "Sandbox";
			}
			return new ConceptualPod.Builder(new ConceptualPod(name, edmName, displayName, description, edmEntityContainerName, visibility, cortanaEnabled));
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000073CC File Offset: 0x000055CC
		public string Name { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000288 RID: 648 RVA: 0x000073D4 File Offset: 0x000055D4
		public string EdmName { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000073DC File Offset: 0x000055DC
		public string DisplayName { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000073E4 File Offset: 0x000055E4
		public string Description { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000073EC File Offset: 0x000055EC
		public string EntityContainerName { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000073F4 File Offset: 0x000055F4
		// (set) Token: 0x0600028D RID: 653 RVA: 0x000073FC File Offset: 0x000055FC
		public IConceptualSchema Schema { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00007405 File Offset: 0x00005605
		public ConceptualEntityVisibilityType Visibility { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000740D File Offset: 0x0000560D
		public bool CortanaEnabled { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00007415 File Offset: 0x00005615
		public string Extends
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007418 File Offset: 0x00005618
		public bool IsDateTable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000741B File Offset: 0x0000561B
		public IReadOnlyList<IConceptualProperty> Properties
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualProperty>();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00007422 File Offset: 0x00005622
		public IReadOnlyList<IConceptualNavigationProperty> NavigationProperties
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualNavigationProperty>();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00007429 File Offset: 0x00005629
		public IReadOnlyList<IConceptualHierarchy> Hierarchies
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualHierarchy>();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00007430 File Offset: 0x00005630
		public IReadOnlyList<IConceptualDisplayFolder> DisplayFolders
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00007437 File Offset: 0x00005637
		public IReadOnlyList<IConceptualColumn> KeyColumns
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000743E File Offset: 0x0000563E
		public IConceptualColumn DefaultLabelColumn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00007441 File Offset: 0x00005641
		public IConceptualColumn DefaultImageColumn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00007444 File Offset: 0x00005644
		public IReadOnlyList<IConceptualProperty> DefaultFieldProperties
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualProperty>();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000744B File Offset: 0x0000564B
		public ConceptualEntityStatistics Statistics
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000744E File Offset: 0x0000564E
		public IReadOnlyList<IConceptualPodParameter> Parameters
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualPodParameter>();
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00007455 File Offset: 0x00005655
		public ConceptualTableType ConceptualResultType
		{
			get
			{
				return ConceptualTableType.Empty;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000745C File Offset: 0x0000565C
		public string StableName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000745F File Offset: 0x0000565F
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007467 File Offset: 0x00005667
		public bool TryGetPropertyByEdmName(string edmName, out IConceptualProperty conceptualProp)
		{
			conceptualProp = null;
			return false;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000746D File Offset: 0x0000566D
		public bool TryGetProperty(string referenceName, out IConceptualProperty conceptualProp)
		{
			conceptualProp = null;
			return false;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007473 File Offset: 0x00005673
		public bool TryGetHierarchy(string name, out IConceptualHierarchy conceptualHierarchy)
		{
			conceptualHierarchy = null;
			return conceptualHierarchy != null;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000747D File Offset: 0x0000567D
		public bool TryGetHierarchyByEdmName(string edmName, out IConceptualHierarchy conceptualHierarchy)
		{
			conceptualHierarchy = null;
			return conceptualHierarchy != null;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007487 File Offset: 0x00005687
		public string GetFullName()
		{
			return this.EntityContainerName + "." + this.EdmName;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000749F File Offset: 0x0000569F
		bool IEquatable<IConceptualEntity>.Equals(IConceptualEntity other)
		{
			return this == other;
		}

		// Token: 0x020002FC RID: 764
		public sealed class Builder : ConceptualBuilderBase<ConceptualPod>
		{
			// Token: 0x06001936 RID: 6454 RVA: 0x0002D602 File Offset: 0x0002B802
			public Builder(ConceptualPod conceptualPod)
				: base(conceptualPod)
			{
			}

			// Token: 0x06001937 RID: 6455 RVA: 0x0002D60B File Offset: 0x0002B80B
			public IConceptualEntity CompleteInitialization(IConceptualSchema schema)
			{
				Contract.CheckValue<IConceptualSchema>(schema, "schema");
				base.ActiveObject.Schema = schema;
				return base.CompleteBaseInitialization();
			}
		}
	}
}
