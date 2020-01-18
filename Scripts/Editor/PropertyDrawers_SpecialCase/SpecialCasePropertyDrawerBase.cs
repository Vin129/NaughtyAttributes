﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor
{
	public abstract class SpecialCasePropertyDrawerBase
	{
		public void OnGUI(SerializedProperty property)
		{
			bool enabled = PropertyUtility.IsEnabled(property);
			GUI.enabled = enabled;
			OnGUI_Internal(property);
			GUI.enabled = true;
		}

		protected abstract void OnGUI_Internal(SerializedProperty property);
	}

	public static class SpecialCaseDrawerAttributeExtensions
	{
		private static Dictionary<Type, SpecialCasePropertyDrawerBase> _drawersByAttributeType;

		static SpecialCaseDrawerAttributeExtensions()
		{
			_drawersByAttributeType = new Dictionary<Type, SpecialCasePropertyDrawerBase>();
			_drawersByAttributeType[typeof(ReorderableListAttribute)] = ReorderableListPropertyDrawer.Instance;
		}

		public static SpecialCasePropertyDrawerBase GetDrawer(this SpecialCaseDrawerAttribute attr)
		{
			SpecialCasePropertyDrawerBase drawer;
			if (_drawersByAttributeType.TryGetValue(attr.GetType(), out drawer))
			{
				return drawer;
			}
			else
			{
				return null;
			}
		}
	}
}
