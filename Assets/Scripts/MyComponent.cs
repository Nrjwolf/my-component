using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(Image))] // Запрашиваем (создаем) компонент Image
public class MyComponent : MonoBehaviour
{
    // Типы нашего компоненты
    public enum Type
    {
        CAR,
        CLOUD,
        COIN
    }

    // Данные, которые будут изменяться в зависимости от выбранного компонента
    [Serializable]
    public class Data
    {
        public Type type;
        public Sprite sprite;
        public Parametrs parametrs;
    }

    [Serializable]
    public class Parametrs
    {
        public int count;
        public Color color = Color.white;

        public Parametrs(Parametrs obj)
        {
            this.color = obj.color;
            this.count = obj.count;
        }
    }

    [Serializable]
    public class View
    {
        public Image image;
        public Text text;
    }

    public Type type;
    public Parametrs parametrs;
    public List<Data> data;
    [HideInInspector] public View view;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        // Получаем ссылку на визуальные компоненты
        if (view.image == null)
            view.image = GetComponent<Image>();
        if (view.text == null)
            view.text = GetComponentInChildren<Text>();
    }

    public void UpdateView()
    {
        var _data = data.Find(x => x.type == type);
        view.image.sprite = _data.sprite;
        view.image.color = _data.parametrs.color;
        view.text.text = _data.parametrs.count.ToString();
    }

    public void UpdateView(Color color, int count)
    {
        view.image.color = color;
        view.text.text = count.ToString();
    }
}
