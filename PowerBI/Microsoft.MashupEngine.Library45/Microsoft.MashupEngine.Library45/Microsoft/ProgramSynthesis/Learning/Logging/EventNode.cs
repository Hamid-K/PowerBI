using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x0200075E RID: 1886
	internal class EventNode : IEnumerable<KeyValuePair<string, EventAttribute>>, IEnumerable
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000724CC File Offset: 0x000706CC
		public List<EventNode> Children { get; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000724D4 File Offset: 0x000706D4
		public Stopwatch Stopwatch { get; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x000724DC File Offset: 0x000706DC
		private IDictionary<string, EventAttribute> DisplayedAttributes
		{
			get
			{
				return this._attributes.ToImmutableDictionary<string, EventAttribute>().Add("elapsed", new EventAttribute("elapsed", this.Elapsed.ToString()));
			}
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x0007251C File Offset: 0x0007071C
		public EventNode(string name, IDictionary<string, EventAttribute> attributes, List<EventNode> children)
		{
			this.Name = name;
			this._attributes = attributes;
			this.Children = children;
			this.Stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x00072544 File Offset: 0x00070744
		public EventNode(string name, IDictionary<string, EventAttribute> attributes, params EventNode[] children)
			: this(name, attributes, children.ToList<EventNode>())
		{
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x00072554 File Offset: 0x00070754
		public EventNode(string name, IDictionary<string, object> attributes, IEnumerable<EventNode> children)
			: this(name, attributes.ToDictionary((KeyValuePair<string, object> kvp) => kvp.Key, (KeyValuePair<string, object> kvp) => new EventAttribute(kvp.Key, kvp.Value)), (children as List<EventNode>) ?? children.ToList<EventNode>())
		{
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000725BC File Offset: 0x000707BC
		public EventNode(string name, IDictionary<string, object> attributes, params EventNode[] children)
			: this(name, attributes, children.AsEnumerable<EventNode>())
		{
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000725CC File Offset: 0x000707CC
		public EventNode(string name, params EventNode[] children)
			: this(name, new Dictionary<string, EventAttribute>(), children)
		{
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000725DB File Offset: 0x000707DB
		public EventNode(string name, IEnumerable<EventNode> children)
			: this(name, new Dictionary<string, EventAttribute>(), (children as List<EventNode>) ?? children.ToList<EventNode>())
		{
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x000725F9 File Offset: 0x000707F9
		public string Name { get; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x00072601 File Offset: 0x00070801
		// (set) Token: 0x0600284D RID: 10317 RVA: 0x00072609 File Offset: 0x00070809
		public EventNode Parent { get; private set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x00072612 File Offset: 0x00070812
		public TimeSpan Elapsed
		{
			get
			{
				return this.Stopwatch.Elapsed;
			}
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x0007261F File Offset: 0x0007081F
		public void Add(string attribute, object value)
		{
			this.Add(new EventAttribute(attribute, value));
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0007262E File Offset: 0x0007082E
		public void Add(string attribute, string value)
		{
			this.Add(attribute, value);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00072638 File Offset: 0x00070838
		public void Add<T>(string attribute, IEnumerable<T> value)
		{
			this.Add(new CollectionEventAttribute<T>(attribute, value));
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00072647 File Offset: 0x00070847
		public void Add(string attribute, Spec value)
		{
			this.Add(new SpecEventAttribute(attribute, value));
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00072656 File Offset: 0x00070856
		public void Add(string attribute, ProgramSet value)
		{
			this.Add(new ProgramSetEventAttribute(attribute, value));
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x00072665 File Offset: 0x00070865
		public void Add(string attribute, State value)
		{
			this.Add(new StateEventAttribute(attribute, value));
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x00072674 File Offset: 0x00070874
		private void Add(EventAttribute attribute)
		{
			this._attributes[attribute.Name] = attribute;
		}

		// Token: 0x170006FF RID: 1791
		public object this[string attribute]
		{
			get
			{
				EventAttribute eventAttribute;
				if (!this._attributes.TryGetValue(attribute, out eventAttribute))
				{
					return null;
				}
				return eventAttribute.Value;
			}
			set
			{
				Spec spec = value as Spec;
				if (spec != null)
				{
					this.Add(attribute, spec);
					return;
				}
				ProgramSet programSet = value as ProgramSet;
				if (programSet != null)
				{
					this.Add(attribute, programSet);
					return;
				}
				State state = value as State;
				if (state == null)
				{
					if (!(value is string))
					{
						if (value is IEnumerable)
						{
							this.Add<object>(attribute, value.ToEnumerable<object>());
							return;
						}
					}
					this.Add(attribute, value);
					return;
				}
				this.Add(attribute, state);
			}
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x0007271E File Offset: 0x0007091E
		public void AddChild(EventNode child)
		{
			this.Children.Add(child);
			child.Parent = this;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x00072733 File Offset: 0x00070933
		public IEnumerator<KeyValuePair<string, EventAttribute>> GetEnumerator()
		{
			return this._attributes.GetEnumerator();
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x00072740 File Offset: 0x00070940
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x00072748 File Offset: 0x00070948
		public EventNode Root
		{
			get
			{
				if (this._root != null)
				{
					return this._root;
				}
				EventNode eventNode = this;
				while (eventNode.Parent != null)
				{
					eventNode = eventNode.Parent;
				}
				return this._root = eventNode;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x00072784 File Offset: 0x00070984
		private Dictionary<object, int> IdentityCache
		{
			get
			{
				if (this._identityCache != null)
				{
					return this._identityCache;
				}
				return this._identityCache = ((this.Root == this) ? new Dictionary<object, int>(IdentityEquality.Comparer) : this.Root._identityCache);
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000727CC File Offset: 0x000709CC
		public XElement ToXML()
		{
			IEnumerable<XObject> enumerable = this.DisplayedAttributes.Values.Select((EventAttribute a) => a.ToXML(this.IdentityCache));
			XName xname = this.Name;
			object[] array = new object[2];
			array[0] = enumerable;
			array[1] = this.Children.Select((EventNode e) => e.ToXML());
			XElement xelement = new XElement(xname, array);
			if (this.Root != this)
			{
				return xelement;
			}
			IEnumerable<XElement> enumerable2 = this.IdentityCache.Select((KeyValuePair<object, int> kvp) => new XElement("Object", new XCData(kvp.Key.ToString())).WithAttribute("id", "##" + kvp.Value.ToString()).WithAttribute("type", kvp.Key.GetType()));
			xelement.AddFirst(new XElement("_IDENTITIES", enumerable2));
			return xelement;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0007288C File Offset: 0x00070A8C
		public override string ToString()
		{
			IEnumerable<XObject> enumerable = this.DisplayedAttributes.Values.Select((EventAttribute a) => a.ToXML(this.IdentityCache));
			return new XElement(this.Name, enumerable).ToString();
		}

		// Token: 0x04001390 RID: 5008
		private readonly IDictionary<string, EventAttribute> _attributes;

		// Token: 0x04001395 RID: 5013
		private EventNode _root;

		// Token: 0x04001396 RID: 5014
		private Dictionary<object, int> _identityCache;
	}
}
