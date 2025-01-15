using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D2 RID: 210
	public class PublishedEvent
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0001C56C File Offset: 0x0001C56C
		public string Name
		{
			get
			{
				return this.m_metadata.Name;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0001C584 File Offset: 0x0001C584
		public Guid UUID
		{
			get
			{
				return this.m_metadata.UUID;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0001C59C File Offset: 0x0001C59C
		public IPackage Package
		{
			get
			{
				return this.m_metadata.Package;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0001C5B4 File Offset: 0x0001C5B4
		public IEventMetadata Metadata
		{
			get
			{
				return this.m_metadata;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0001C5C8 File Offset: 0x0001C5C8
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0001C5DC File Offset: 0x0001C5DC
		public DateTimeOffset Timestamp { get; internal set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0001C5F0 File Offset: 0x0001C5F0
		public PublishedEvent.FieldList Fields
		{
			get
			{
				return this.m_fieldList;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0001C604 File Offset: 0x0001C604
		public PublishedEvent.ActionList Actions
		{
			get
			{
				return this.m_actionList;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0001C618 File Offset: 0x0001C618
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0001C62C File Offset: 0x0001C62C
		public EventLocator Location { get; internal set; }

		// Token: 0x060002A5 RID: 677 RVA: 0x0001C640 File Offset: 0x0001C640
		internal PublishedEvent(int fieldCount, IEventMetadata metadata)
		{
			this.m_fields = new PublishedEventField[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				this.m_fields[i] = new PublishedEventField(i, metadata);
			}
			this.m_metadata = metadata;
			this.m_actions = new List<PublishedAction>();
			this.m_fieldList = new PublishedEvent.FieldList(this);
			this.m_actionList = new PublishedEvent.ActionList(this);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0001C6A4 File Offset: 0x0001C6A4
		internal void AddAction(PublishedAction action)
		{
			this.m_actions.Add(action);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001C6C0 File Offset: 0x0001C6C0
		internal void SetField(int fieldIndex, object value)
		{
			this.m_fields[fieldIndex].Value = value;
		}

		// Token: 0x04000292 RID: 658
		private IEventMetadata m_metadata;

		// Token: 0x04000293 RID: 659
		private PublishedEventField[] m_fields;

		// Token: 0x04000294 RID: 660
		private List<PublishedAction> m_actions;

		// Token: 0x04000295 RID: 661
		private PublishedEvent.FieldList m_fieldList;

		// Token: 0x04000296 RID: 662
		private PublishedEvent.ActionList m_actionList;

		// Token: 0x020000DE RID: 222
		public class FieldList : IEnumerable
		{
			// Token: 0x060002FD RID: 765 RVA: 0x0001CF50 File Offset: 0x0001CF50
			internal FieldList(PublishedEvent parent)
			{
				this.m_parent = parent;
			}

			// Token: 0x060002FE RID: 766 RVA: 0x0001CF6C File Offset: 0x0001CF6C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_parent.m_fields.GetEnumerator();
			}

			// Token: 0x1700005D RID: 93
			public PublishedEventField this[int fieldIndex]
			{
				get
				{
					return this.m_parent.m_fields[fieldIndex];
				}
			}

			// Token: 0x1700005E RID: 94
			public PublishedEventField this[string fieldName]
			{
				get
				{
					PublishedEventField publishedEventField;
					if (this.TryGetValue(fieldName, out publishedEventField))
					{
						return publishedEventField;
					}
					throw new IndexOutOfRangeException(fieldName);
				}
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000301 RID: 769 RVA: 0x0001CFC8 File Offset: 0x0001CFC8
			public int Count
			{
				get
				{
					return this.m_parent.m_fields.Count<PublishedEventField>();
				}
			}

			// Token: 0x06000302 RID: 770 RVA: 0x0001CFE8 File Offset: 0x0001CFE8
			public bool TryGetValue(string fieldName, out PublishedEventField value)
			{
				ReadOnlyCollection<IEventFieldMetadata> fields = this.m_parent.m_metadata.Fields;
				for (int i = 0; i < fields.Count; i++)
				{
					if (fields[i].Name == fieldName)
					{
						value = this.m_parent.m_fields[i];
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x040002B9 RID: 697
			private PublishedEvent m_parent;
		}

		// Token: 0x020000DF RID: 223
		public class ActionList : IEnumerable
		{
			// Token: 0x17000060 RID: 96
			public PublishedAction this[string actionName]
			{
				get
				{
					PublishedAction publishedAction;
					if (this.TryGetValue(actionName, out publishedAction))
					{
						return publishedAction;
					}
					throw new IndexOutOfRangeException(actionName);
				}
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0001D060 File Offset: 0x0001D060
			public bool TryGetValue(string actionName, out PublishedAction value)
			{
				bool flag = false;
				string[] array = actionName.Split(new char[] { '.' });
				int num = array.Length;
				if (num < 1 || num > 3)
				{
					value = null;
					return flag;
				}
				string text = array[num - 1];
				string text2 = ((num > 1) ? array[num - 2] : "");
				string text3 = ((num > 2) ? array[num - 3] : "");
				foreach (PublishedAction publishedAction in this.m_parent.m_actions)
				{
					if (publishedAction.Name == text)
					{
						flag = true;
						if (num > 1)
						{
							flag = publishedAction.Package.Name == text2;
							if (flag && num > 2)
							{
								flag = publishedAction.Package.ModuleId.ToString() == text3;
							}
						}
						if (flag)
						{
							value = publishedAction;
							return flag;
						}
					}
				}
				value = null;
				return flag;
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000305 RID: 773 RVA: 0x0001D174 File Offset: 0x0001D174
			public int Count
			{
				get
				{
					return this.m_parent.m_actions.Count<PublishedAction>();
				}
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0001D194 File Offset: 0x0001D194
			public IEnumerator GetEnumerator()
			{
				return this.m_parent.m_actions.GetEnumerator();
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0001D1B8 File Offset: 0x0001D1B8
			internal ActionList(PublishedEvent parent)
			{
				this.m_parent = parent;
			}

			// Token: 0x040002BA RID: 698
			private PublishedEvent m_parent;
		}
	}
}
