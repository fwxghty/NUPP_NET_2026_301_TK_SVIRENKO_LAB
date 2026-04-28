using SvirenkoLab01;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


    public class CrudService<T> : ICrudService<T> where T : IEntity
    {
        private List<T> _items;

        public CrudService()
        {
            _items = new List<T>();
        }

        public void Create(T element)
        {
            _items.Add(element);
        }

        public T Read(Guid id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> ReadAll()
        {
            return _items;
        }

        public void Update(T element)
        {
            var existing = Read(element.Id);
            if (existing != null)
            {
                var index = _items.IndexOf(existing);
                _items[index] = element;
            }
        }

        public void Remove(T element)
        {
            var existing = Read(element.Id);
            if (existing != null)
            {
                _items.Remove(existing);
            }
        }

        // Серіалізація у файл
        public void Save(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_items, options);
            File.WriteAllText(filePath, jsonString);
        }

        // Десеріалізація з файлу
        public void Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                _items = JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            else
            {
                _items = new List<T>();
            }
        }
    }