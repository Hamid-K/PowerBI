using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000038 RID: 56
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class CheckedCollection<T> : Collection<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x06000238 RID: 568 RVA: 0x000091AA File Offset: 0x000073AA
		public CheckedCollection()
			: base(new List<T>())
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000091B7 File Offset: 0x000073B7
		public CheckedCollection(int capacity)
			: base(new List<T>(capacity))
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000091C5 File Offset: 0x000073C5
		public CheckedCollection(IEnumerable<T> items)
			: base(new List<T>(items))
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000091D3 File Offset: 0x000073D3
		public virtual bool IsReadOnly
		{
			get
			{
				return this.m_readOnly;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000091DB File Offset: 0x000073DB
		public bool IsEmpty
		{
			get
			{
				return base.Count == 0;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000091E6 File Offset: 0x000073E6
		protected new List<T> Items
		{
			get
			{
				return (List<T>)base.Items;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000091F3 File Offset: 0x000073F3
		public new List<T>.Enumerator GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009200 File Offset: 0x00007400
		public void AddRange(IEnumerable<T> items)
		{
			this.InsertRange(base.Count, items);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009210 File Offset: 0x00007410
		public void InsertRange(int index, IEnumerable<T> items)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (items == null)
			{
				throw new ArgumentNullException();
			}
			this.CheckWriteable();
			int count = base.Count;
			ICollection<T> collection = items as ICollection<T>;
			IList<T> list = items as IList<T>;
			if (collection != null && this.Items.Capacity < count + collection.Count)
			{
				this.Items.Capacity = count + collection.Count;
			}
			try
			{
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						this.InsertItem(index + i, list[i]);
					}
				}
				else
				{
					int num = index;
					foreach (T t in items)
					{
						this.InsertItem(num++, t);
					}
				}
			}
			catch
			{
				while (base.Count > count)
				{
					this.RemoveItem(index);
				}
				throw;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009318 File Offset: 0x00007518
		public void RemoveRange(int index, int count)
		{
			if (index < 0 || index > base.Count || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (index + count > base.Count)
			{
				throw new ArgumentException();
			}
			this.CheckWriteable();
			for (int i = 0; i < count; i++)
			{
				this.RemoveItem(index);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009366 File Offset: 0x00007566
		protected override void InsertItem(int index, T item)
		{
			this.CheckWriteable();
			this.ValidateItem(item);
			base.InsertItem(index, item);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000937D File Offset: 0x0000757D
		protected override void SetItem(int index, T item)
		{
			this.CheckWriteable();
			this.ValidateItem(item);
			base.SetItem(index, item);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009394 File Offset: 0x00007594
		protected override void RemoveItem(int index)
		{
			this.CheckWriteable();
			base.RemoveItem(index);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000093A3 File Offset: 0x000075A3
		protected override void ClearItems()
		{
			this.CheckWriteable();
			base.ClearItems();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000093B1 File Offset: 0x000075B1
		protected virtual void ValidateItem(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException();
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000093C1 File Offset: 0x000075C1
		protected void CheckWriteable()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(DevExceptionMessages.ReadOnly);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000093D6 File Offset: 0x000075D6
		protected void SetReadOnlyIndicator()
		{
			this.m_readOnly = true;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000093E0 File Offset: 0x000075E0
		internal static bool LazyCloneItems<I>(CheckedCollection<I> collection, CheckedCollection<I> clone, bool stopOnFailure, SemanticModel newModel) where I : ILazyCloneable<I>
		{
			foreach (I i in collection)
			{
				I i2 = i.CloneFor(newModel);
				if (i2 != null)
				{
					clone.Add(i2);
				}
				else if (stopOnFailure)
				{
					return false;
				}
			}
			clone.SetReadOnlyIndicator();
			return true;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009458 File Offset: 0x00007658
		internal static void CompileItems<I>(CheckedCollection<I> collection, CompilationContext ctx) where I : ICompileable
		{
			foreach (I i in collection)
			{
				i.Compile(ctx);
			}
			if (ctx.ShouldPersist)
			{
				collection.SetReadOnlyIndicator();
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000094B8 File Offset: 0x000076B8
		internal virtual IDisposable AllowWriteOperations()
		{
			return new CheckedCollection<T>.AllowWriteOperationImpl(this);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000094C8 File Offset: 0x000076C8
		internal virtual void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(CheckedCollection<T>.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.ReadOnly)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				writer.Write(this.m_readOnly);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009538 File Offset: 0x00007738
		internal virtual void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(CheckedCollection<T>.Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName != MemberName.ReadOnly)
				{
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
				this.m_readOnly = reader.ReadBoolean();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000095A5 File Offset: 0x000077A5
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref CheckedCollection<T>.__declaration, CheckedCollection<T>.__declarationLock, () => new Declaration(ObjectType.CheckedCollection, ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ReadOnly, Token.Boolean)
				}));
			}
		}

		// Token: 0x0400013F RID: 319
		private bool m_readOnly;

		// Token: 0x04000140 RID: 320
		private static Declaration __declaration;

		// Token: 0x04000141 RID: 321
		private static readonly object __declarationLock = new object();

		// Token: 0x02000119 RID: 281
		private struct AllowWriteOperationImpl : IDisposable
		{
			// Token: 0x06000D94 RID: 3476 RVA: 0x0002CE5A File Offset: 0x0002B05A
			internal AllowWriteOperationImpl(CheckedCollection<T> checkedCollection)
			{
				this.m_checkedCollection = checkedCollection;
				this.m_savedReadOnly = checkedCollection.m_readOnly;
				if (checkedCollection.m_readOnly)
				{
					checkedCollection.m_readOnly = false;
				}
			}

			// Token: 0x06000D95 RID: 3477 RVA: 0x0002CE7E File Offset: 0x0002B07E
			public void Dispose()
			{
				this.m_checkedCollection.m_readOnly = this.m_savedReadOnly;
			}

			// Token: 0x040005A4 RID: 1444
			private readonly CheckedCollection<T> m_checkedCollection;

			// Token: 0x040005A5 RID: 1445
			private readonly bool m_savedReadOnly;
		}
	}
}
