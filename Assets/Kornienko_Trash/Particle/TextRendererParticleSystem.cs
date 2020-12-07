﻿using System;
using UnityEngine;
using System.Collections.Generic;
using ExampleTemplate;
using Model.Enemy;

namespace Assets.Scripts.ParticleScore
{
    [RequireComponent(typeof(ParticleSystem))]
    class TextRendererParticleSystem : MonoBehaviour
    {
        #region Fields

        public SymbolsTextureData SymbolsTextureData;

        private ParticleSystemRenderer particleSystemRenderer;
        private new ParticleSystem particleSystem;


        #endregion

        private void OnEnable()
        {
            BodyPoint.OnScoreChanged += SpawnParticle;
        }
        private void OnDisable()
        {
            BodyPoint.OnScoreChanged -= SpawnParticle;
        }

        #region Methods
        public void SpawnParticle(Vector3 position, float amount, Color color)
        {
            var amountInt = Mathf.RoundToInt(amount);
            if (amountInt == 0) return;
            var str = amountInt.ToString();
            if (amountInt > 0) str = "+" + str;
            SpawnParticle(position, str, color);
        }

        public void SpawnParticle(Vector3 position, string message, Color color)
        {
            var texCords = new Vector2[24]; //массив из 24 элемент - 23 символа + длина сообщения
            var messageLenght = Mathf.Min(23, message.Length);
            texCords[texCords.Length - 1] = new Vector2(0, messageLenght);
            for (int i = 0; i < texCords.Length; i++)
            {
                if (i >= messageLenght) break;
                //Вызываем метод GetTextureCoordinates() из SymbolsTextureData для получения позиции символа
                texCords[i] = SymbolsTextureData.GetTextureCoordinates(message[i]);
            }

            var custom1Data = CreateCustomData(texCords);
            var custom2Data = CreateCustomData(texCords, 12);

            if (particleSystem == null) particleSystem = GetComponent<ParticleSystem>();

            if (particleSystemRenderer == null)
            {
                //Если ссылка на ParticleSystemRenderer, кэшируем и убеждаемся в наличии нужных потоков
                particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
                var streams = new List<ParticleSystemVertexStream>();
                particleSystemRenderer.GetActiveVertexStreams(streams);
                //Добавляем лишний поток Vector2(UV2, SizeXY, etc.), чтобы координаты в скрипте соответствовали координатам в шейдере
                if (!streams.Contains(ParticleSystemVertexStream.UV2)) streams.Add(ParticleSystemVertexStream.UV2);
                if (!streams.Contains(ParticleSystemVertexStream.Custom1XYZW)) streams.Add(ParticleSystemVertexStream.Custom1XYZW);
                if (!streams.Contains(ParticleSystemVertexStream.Custom2XYZW)) streams.Add(ParticleSystemVertexStream.Custom2XYZW);
                particleSystemRenderer.SetActiveVertexStreams(streams);
            }

            //Инициализируем параметры эммишена
            //Цвет и позицию получаем из параметров метода
            //Устанавливаем startSize3D по X, чтобы символы не растягивались и не сжимались
            //при изменении длины сообщения
            var emitParams = new ParticleSystem.EmitParams
            {
                startColor = color,
                position = position,
                applyShapeToPosition = true,
                startSize3D = new Vector3(messageLenght, 1, 1)
            };
            //Если мы хотим создавать частицы разного размера, то в параметрах SpawnParticle неоходимо
            //передать нужное значение startSize
            //Непосредственно спаун частицы
            particleSystem.Emit(emitParams, 1);

            //Передаем кастомные данные в нужные потоки
            var customData = new List<Vector4>();
            //Получаем поток ParticleSystemCustomData.Custom1 из ParticleSystem
            particleSystem.GetCustomParticleData(customData, ParticleSystemCustomData.Custom1);
            //Меняем данные последнего элемент, т.е. той частицы, которую мы только что создали
            customData[customData.Count - 1] = custom1Data;
            //Возвращаем данные в ParticleSystem
            particleSystem.SetCustomParticleData(customData, ParticleSystemCustomData.Custom1);

            //Аналогично для ParticleSystemCustomData.Custom2
            particleSystem.GetCustomParticleData(customData, ParticleSystemCustomData.Custom2);
            customData[customData.Count - 1] = custom2Data;
            particleSystem.SetCustomParticleData(customData, ParticleSystemCustomData.Custom2);
        }

        public float PackFloat(Vector2[] vecs)
        {
            if (vecs == null || vecs.Length == 0) return 0;
            //Поразрядно добавляем значения координат векторов в float
            var result = vecs[0].y * 10000 + vecs[0].x * 100000;
            if (vecs.Length > 1) result += vecs[1].y * 100 + vecs[1].x * 1000;
            if (vecs.Length > 2) result += vecs[2].y + vecs[2].x * 10;
            return result;
        }

        private Vector4 CreateCustomData(Vector2[] texCoords, int offset = 0)
        {
            var data = Vector4.zero;
            for (int i = 0; i < 4; i++)
            {
                var vecs = new Vector2[3];
                for (int j = 0; j < 3; j++)
                {
                    var ind = i * 3 + j + offset;
                    if (texCoords.Length > ind)
                    {
                        vecs[j] = texCoords[ind];
                    }
                    else
                    {
                        data[i] = PackFloat(vecs);
                        i = 5;
                        break;
                    }
                }
                if (i < 4) data[i] = PackFloat(vecs);
            }
            return data;
        }

        #endregion
    }
}
