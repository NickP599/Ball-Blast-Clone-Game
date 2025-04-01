using System;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed; // Скорость движения по горизонтали
    [SerializeField] private float reboundSpeed; // Скорость отскока при столкновении с нижней границей
    [SerializeField] private float gravity; // Сила гравитации
    [SerializeField] private float gravityOffSet; // Смещение для включения гравитации
    [SerializeField] private float rotateSpeed; // Скорость вращения

    private bool useGravity; // Флаг использования гравитации

    private Vector3 velocity; // Вектор скорости

    private void Awake()
    {
        // Устанавливаем начальную горизонтальную скорость в зависимости от позиции объекта
        velocity.x = -Mathf.Sign(transform.position.x) * horizontalSpeed;
    }

    private void Update()
    {
        // Проверяем, нужно ли включить гравитацию
        TryEnableGravity();
        // Выполняем движение объекта
        Move();
    }

    private void Move()
    {
        if (useGravity)
        {
            // Применяем гравитацию к вертикальной скорости
            velocity.y -= gravity * Time.deltaTime;
            // Вращаем объект
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }

        // Обновляем горизонтальную скорость
        velocity.x = Mathf.Sign(velocity.x) * horizontalSpeed;

        // Обновляем позицию объекта
        transform.position += velocity * Time.deltaTime;
    }

    private void TryEnableGravity()
    {
        // Включаем гравитацию, если объект находится в пределах границ уровня с учетом смещения
        if (Math.Abs(transform.position.x) <= Math.Abs(LevelBoundary.Instance.LeftBorder) - gravityOffSet)
        {
            useGravity = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelEdge levelEdge = collision.GetComponent<LevelEdge>();

        if (levelEdge != null)
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                // Отскакиваем от нижней границы
                velocity.y = reboundSpeed;
            }

            if (levelEdge.Type == EdgeType.Left && velocity.x < 0 || levelEdge.Type == EdgeType.Right && velocity.x > 0)
            {
                // Меняем направление горизонтальной скорости при столкновении с боковыми границами
                velocity.x *= -1;
            }
        }
    }

    public void AddVerticalvelocity(float velocity)
    {
        // Добавляем вертикальную скорость
        this.velocity.y += velocity;
    }

    public void SetHorizontalDerection(float direction)
    {
        // Устанавливаем горизонтальное направление движения
        velocity.x = Mathf.Sign(direction) * horizontalSpeed;
    }
}

