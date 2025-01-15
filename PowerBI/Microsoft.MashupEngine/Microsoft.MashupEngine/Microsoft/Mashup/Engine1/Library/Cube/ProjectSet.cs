using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D62 RID: 3426
	internal class ProjectSet : DelegatingSet
	{
		// Token: 0x06005CE8 RID: 23784 RVA: 0x0014210F File Offset: 0x0014030F
		public ProjectSet(Set set, IEnumerable<ICubeObject> objects)
			: this(set, ProjectSet.OrderedDistinct(objects))
		{
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x0014211E File Offset: 0x0014031E
		private ProjectSet(Set set, ICubeObject[] objects)
			: base(set)
		{
			this.objects = objects;
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x0014212E File Offset: 0x0014032E
		protected override Set New(Set set)
		{
			return new ProjectSet(set, this.objects);
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x06005CEB RID: 23787 RVA: 0x0014213C File Offset: 0x0014033C
		public override SetKind Kind
		{
			get
			{
				return SetKind.Project;
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06005CEC RID: 23788 RVA: 0x00142140 File Offset: 0x00140340
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x06005CED RID: 23789 RVA: 0x00142148 File Offset: 0x00140348
		public IEnumerable<ICubeObject> Objects
		{
			get
			{
				return this.objects;
			}
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x00142150 File Offset: 0x00140350
		public override IEnumerable<ICubeObject> GetResultObjects()
		{
			return base.GetResultObjects().Concat(this.objects);
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x00142163 File Offset: 0x00140363
		public override Set Project(IEnumerable<ICubeObject> objects)
		{
			return new ProjectSet(this.set, this.objects.Concat(objects));
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x0014217C File Offset: 0x0014037C
		public override Set NewScope(string scope)
		{
			return new ProjectSet(this.set.NewScope(scope), this.objects.Select((ICubeObject o) => o.NewScope(scope)));
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x001421C3 File Offset: 0x001403C3
		public bool Equals(ProjectSet other)
		{
			return other != null && this.set.Equals(other.set) && this.objects.SetEquals(other.objects);
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x001421EE File Offset: 0x001403EE
		public override bool Equals(object other)
		{
			return this.Equals(other as ProjectSet);
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x001421FC File Offset: 0x001403FC
		public override int GetHashCode()
		{
			return 37 * this.set.GetHashCode() + 5011 * this.objects.SetGetHashCode<ICubeObject>();
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x00142220 File Offset: 0x00140420
		private static ICubeObject[] OrderedDistinct(IEnumerable<ICubeObject> objects)
		{
			ArrayBuilder<ICubeObject> arrayBuilder = default(ArrayBuilder<ICubeObject>);
			HashSet<ICubeObject> hashSet = new HashSet<ICubeObject>();
			foreach (ICubeObject cubeObject in objects)
			{
				if (hashSet.Add(cubeObject))
				{
					arrayBuilder.Add(cubeObject);
				}
			}
			return arrayBuilder.ToArray();
		}

		// Token: 0x04003350 RID: 13136
		private readonly ICubeObject[] objects;
	}
}
