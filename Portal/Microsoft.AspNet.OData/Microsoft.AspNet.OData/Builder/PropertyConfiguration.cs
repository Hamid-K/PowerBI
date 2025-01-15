using System;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200013D RID: 317
	public abstract class PropertyConfiguration
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0002C6B4 File Offset: 0x0002A8B4
		protected PropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
		{
			if (property == null)
			{
				throw Error.ArgumentNull("property");
			}
			if (declaringType == null)
			{
				throw Error.ArgumentNull("declaringType");
			}
			this.PropertyInfo = property;
			this.DeclaringType = declaringType;
			this.AddedExplicitly = true;
			this._name = property.Name;
			this.QueryConfiguration = new QueryConfiguration();
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0002C715 File Offset: 0x0002A915
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0002C71D File Offset: 0x0002A91D
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._name = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0002C72F File Offset: 0x0002A92F
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0002C737 File Offset: 0x0002A937
		public StructuralTypeConfiguration DeclaringType { get; private set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002C740 File Offset: 0x0002A940
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0002C748 File Offset: 0x0002A948
		public PropertyInfo PropertyInfo { get; private set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B3D RID: 2877
		public abstract Type RelatedClrType { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B3E RID: 2878
		public abstract PropertyKind Kind { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0002C751 File Offset: 0x0002A951
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0002C759 File Offset: 0x0002A959
		public bool AddedExplicitly { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0002C762 File Offset: 0x0002A962
		public bool IsRestricted
		{
			get
			{
				return this.NotFilterable || this.NotSortable || this.NotNavigable || this.NotExpandable || this.NotCountable || this.AutoExpand;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002C794 File Offset: 0x0002A994
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0002C79C File Offset: 0x0002A99C
		public bool NotFilterable { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0002C7A5 File Offset: 0x0002A9A5
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x0002C7AD File Offset: 0x0002A9AD
		public bool AutoExpand { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002C7B6 File Offset: 0x0002A9B6
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x0002C7BE File Offset: 0x0002A9BE
		public bool DisableAutoExpandWhenSelectIsPresent { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002C7C7 File Offset: 0x0002A9C7
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0002C7CF File Offset: 0x0002A9CF
		public bool NonFilterable
		{
			get
			{
				return this.NotFilterable;
			}
			set
			{
				this.NotFilterable = value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0002C7D8 File Offset: 0x0002A9D8
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0002C7E0 File Offset: 0x0002A9E0
		public bool NotSortable { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0002C7E9 File Offset: 0x0002A9E9
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0002C7F1 File Offset: 0x0002A9F1
		public bool Unsortable
		{
			get
			{
				return this.NotSortable;
			}
			set
			{
				this.NotSortable = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002C7FA File Offset: 0x0002A9FA
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0002C802 File Offset: 0x0002AA02
		public bool NotNavigable { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002C80B File Offset: 0x0002AA0B
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0002C813 File Offset: 0x0002AA13
		public bool NotExpandable { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002C81C File Offset: 0x0002AA1C
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0002C824 File Offset: 0x0002AA24
		public bool NotCountable { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002C82D File Offset: 0x0002AA2D
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002C835 File Offset: 0x0002AA35
		public int Order { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002C83E File Offset: 0x0002AA3E
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0002C846 File Offset: 0x0002AA46
		public QueryConfiguration QueryConfiguration { get; set; }

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002C84F File Offset: 0x0002AA4F
		public PropertyConfiguration IsNotFilterable()
		{
			this.NotFilterable = true;
			return this;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002C859 File Offset: 0x0002AA59
		public PropertyConfiguration IsNonFilterable()
		{
			return this.IsNotFilterable();
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002C861 File Offset: 0x0002AA61
		public PropertyConfiguration IsFilterable()
		{
			this.NotFilterable = false;
			return this;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002C86B File Offset: 0x0002AA6B
		public PropertyConfiguration IsNotSortable()
		{
			this.NotSortable = true;
			return this;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002C875 File Offset: 0x0002AA75
		public PropertyConfiguration IsUnsortable()
		{
			return this.IsNotSortable();
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002C87D File Offset: 0x0002AA7D
		public PropertyConfiguration IsSortable()
		{
			this.NotSortable = false;
			return this;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002C887 File Offset: 0x0002AA87
		public PropertyConfiguration IsNotNavigable()
		{
			this.IsNotSortable();
			this.IsNotFilterable();
			this.NotNavigable = true;
			return this;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002C89F File Offset: 0x0002AA9F
		public PropertyConfiguration IsNavigable()
		{
			this.NotNavigable = false;
			return this;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002C8A9 File Offset: 0x0002AAA9
		public PropertyConfiguration IsNotExpandable()
		{
			this.NotExpandable = true;
			return this;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002C8B3 File Offset: 0x0002AAB3
		public PropertyConfiguration IsExpandable()
		{
			this.NotExpandable = false;
			return this;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002C8BD File Offset: 0x0002AABD
		public PropertyConfiguration IsNotCountable()
		{
			this.NotCountable = true;
			return this;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002C8C7 File Offset: 0x0002AAC7
		public PropertyConfiguration IsCountable()
		{
			this.NotCountable = false;
			return this;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002C8D1 File Offset: 0x0002AAD1
		public PropertyConfiguration Count()
		{
			this.QueryConfiguration.SetCount(true);
			return this;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002C8E0 File Offset: 0x0002AAE0
		public PropertyConfiguration Count(QueryOptionSetting queryOptionSetting)
		{
			this.QueryConfiguration.SetCount(queryOptionSetting == QueryOptionSetting.Allowed);
			return this;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002C8F2 File Offset: 0x0002AAF2
		public PropertyConfiguration OrderBy(QueryOptionSetting setting, params string[] properties)
		{
			this.QueryConfiguration.SetOrderBy(properties, setting == QueryOptionSetting.Allowed);
			return this;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002C905 File Offset: 0x0002AB05
		public PropertyConfiguration OrderBy(params string[] properties)
		{
			this.QueryConfiguration.SetOrderBy(properties, true);
			return this;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002C915 File Offset: 0x0002AB15
		public PropertyConfiguration OrderBy(QueryOptionSetting setting)
		{
			this.QueryConfiguration.SetOrderBy(null, setting == QueryOptionSetting.Allowed);
			return this;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002C928 File Offset: 0x0002AB28
		public PropertyConfiguration OrderBy()
		{
			this.QueryConfiguration.SetOrderBy(null, true);
			return this;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002C938 File Offset: 0x0002AB38
		public PropertyConfiguration Filter(QueryOptionSetting setting, params string[] properties)
		{
			this.QueryConfiguration.SetFilter(properties, setting == QueryOptionSetting.Allowed);
			return this;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002C94B File Offset: 0x0002AB4B
		public PropertyConfiguration Filter(params string[] properties)
		{
			this.QueryConfiguration.SetFilter(properties, true);
			return this;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002C95B File Offset: 0x0002AB5B
		public PropertyConfiguration Filter(QueryOptionSetting setting)
		{
			this.QueryConfiguration.SetFilter(null, setting == QueryOptionSetting.Allowed);
			return this;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002C96E File Offset: 0x0002AB6E
		public PropertyConfiguration Filter()
		{
			this.QueryConfiguration.SetFilter(null, true);
			return this;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002C97E File Offset: 0x0002AB7E
		public PropertyConfiguration Select(SelectExpandType selectType, params string[] properties)
		{
			this.QueryConfiguration.SetSelect(properties, selectType);
			return this;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002C98E File Offset: 0x0002AB8E
		public PropertyConfiguration Select(params string[] properties)
		{
			this.QueryConfiguration.SetSelect(properties, SelectExpandType.Allowed);
			return this;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002C99E File Offset: 0x0002AB9E
		public PropertyConfiguration Select(SelectExpandType selectType)
		{
			this.QueryConfiguration.SetSelect(null, selectType);
			return this;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002C9AE File Offset: 0x0002ABAE
		public PropertyConfiguration Select()
		{
			this.QueryConfiguration.SetSelect(null, SelectExpandType.Allowed);
			return this;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002C9BE File Offset: 0x0002ABBE
		public PropertyConfiguration Page(int? maxTopValue, int? pageSizeValue)
		{
			this.QueryConfiguration.SetMaxTop(maxTopValue);
			this.QueryConfiguration.SetPageSize(pageSizeValue);
			return this;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002C9DC File Offset: 0x0002ABDC
		public PropertyConfiguration Page()
		{
			this.QueryConfiguration.SetMaxTop(null);
			this.QueryConfiguration.SetPageSize(null);
			return this;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002CA12 File Offset: 0x0002AC12
		public PropertyConfiguration Expand(int maxDepth, SelectExpandType expandType, params string[] properties)
		{
			this.QueryConfiguration.SetExpand(properties, new int?(maxDepth), expandType);
			return this;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002CA28 File Offset: 0x0002AC28
		public PropertyConfiguration Expand(params string[] properties)
		{
			this.QueryConfiguration.SetExpand(properties, null, SelectExpandType.Allowed);
			return this;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002CA4C File Offset: 0x0002AC4C
		public PropertyConfiguration Expand(int maxDepth, params string[] properties)
		{
			this.QueryConfiguration.SetExpand(properties, new int?(maxDepth), SelectExpandType.Allowed);
			return this;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002CA64 File Offset: 0x0002AC64
		public PropertyConfiguration Expand(SelectExpandType expandType, params string[] properties)
		{
			this.QueryConfiguration.SetExpand(properties, null, expandType);
			return this;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002CA88 File Offset: 0x0002AC88
		public PropertyConfiguration Expand(SelectExpandType expandType, int maxDepth)
		{
			this.QueryConfiguration.SetExpand(null, new int?(maxDepth), expandType);
			return this;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002CA9E File Offset: 0x0002AC9E
		public PropertyConfiguration Expand(int maxDepth)
		{
			this.QueryConfiguration.SetExpand(null, new int?(maxDepth), SelectExpandType.Allowed);
			return this;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002CAB4 File Offset: 0x0002ACB4
		public PropertyConfiguration Expand(SelectExpandType expandType)
		{
			this.QueryConfiguration.SetExpand(null, null, expandType);
			return this;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
		public PropertyConfiguration Expand()
		{
			this.QueryConfiguration.SetExpand(null, null, SelectExpandType.Allowed);
			return this;
		}

		// Token: 0x0400037F RID: 895
		private string _name;
	}
}
