﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Zone.Campaign
{
    public static class XmlNodeExtensions
    {
        public static XmlAttribute AppendAttribute(this XmlNode node, string name, string value = null)
        {
            var ownerDocument = node.OwnerDocument;
            if (ownerDocument == null)
            {
                throw new ArgumentException("Node has no parent document.");
            }

            var attribute = ownerDocument.CreateAttribute(name);
            node.Attributes.Append(attribute);
            if (value != null)
            {
                attribute.InnerText = value;
            }

            return attribute;
        }

        public static XmlElement AppendChild(this XmlNode node, string name)
        {
            return node.AppendChildWithValue(name, null, null);
        }

        public static XmlElement AppendChildWithValue(this XmlNode node, string name, string value)
        {
            return node.AppendChildWithValue(name, null, value);
        }

        public static XmlElement AppendChild(this XmlNode node, string qualifiedName, string namespaceUri)
        {
            return node.AppendChildWithValue(qualifiedName, namespaceUri, null);
        }

        public static XmlElement AppendChildWithValue(this XmlNode node, string qualifiedName, string namespaceUri, string value)
        {
            var ownerDocument = node.OwnerDocument;
            if (ownerDocument == null)
            {
                throw new ArgumentException("Node has no parent document.");
            }

            var child = ownerDocument.CreateElement(qualifiedName, namespaceUri);
            node.AppendChild(child);
            if (value != null)
            {
                child.InnerText = value;
            }

            return child;
        }

        public static void RemoveChild(this XmlElement element, string name)
        {
            var child = element.ChildNodes.Cast<XmlNode>().FirstOrDefault(i => i.LocalName == name);
            if (child == null)
            {
                return;
            }

            element.RemoveChild(child);
        }

        public static void RemoveAllAttributesExcept(this XmlElement element, IEnumerable<string> attributesToKeep)
        {
            var attributesToDiscard = element.Attributes.Cast<XmlAttribute>().Where(i => !attributesToKeep.Contains(i.LocalName)).Select(i => i.LocalName).ToArray();
            foreach (var attribute in attributesToDiscard)
            {
                element.RemoveAttribute(attribute);
            }
        }
    }
}