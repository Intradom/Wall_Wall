using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Behavior : MonoBehaviour
{
    public float green_wall_prob = 0.1f;
    public float cyan_wall_prob = 0.05f;
    public float red_wall_prob = 0.05f;
    public float blue_wall_prob = 0.03f;
    public float yellow_wall_prob = 0.02f;
    public float min_change_CD_seconds = 5;
    public float max_change_CD_seconds = 10;
    private class Wall_Color
    {
        public Color color;
        public float prob;

        public Wall_Color(Color c, float p)
        {
            color = c;
            prob = p;
        }
    };

    private Renderer self_renderer;
    private List<Wall_Color> colors = new List<Wall_Color>();
    private Color current_color;
    private float change_CD_seconds;
    private float last_change_time;

    public Color GetColor()
    {
        Color t_color = current_color;

        SetColor(Color.gray);

        return t_color;
    }

    private void SetColor(Color to_set)
    {
        current_color = to_set;
        self_renderer.material.SetColor("_Color", current_color);
        change_CD_seconds = Random.Range(min_change_CD_seconds, max_change_CD_seconds);
        last_change_time = Time.time;
    }

    private void RandomColor()
    {
        float random_val = Random.value;
        float cumulative_random_stack = 0f;
        bool color_set = false;

        foreach (Wall_Color i in colors)
        {
            cumulative_random_stack += i.prob;
            if (random_val < cumulative_random_stack)
            {
                current_color = i.color;
                color_set = true;
                break;
            }
        }
        if (!color_set)
        {
            current_color = Color.grey;
        }

        self_renderer.material.SetColor("_Color", current_color);
        change_CD_seconds = Random.Range(min_change_CD_seconds, max_change_CD_seconds);
        last_change_time = Time.time;
    }

    private void Awake()
    {
        self_renderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        colors.Add(new Wall_Color(Color.green, green_wall_prob));
        colors.Add(new Wall_Color(Color.cyan, cyan_wall_prob));
        colors.Add(new Wall_Color(Color.red, red_wall_prob));
        colors.Add(new Wall_Color(Color.blue, blue_wall_prob));
        colors.Add(new Wall_Color(Color.yellow, yellow_wall_prob));

        RandomColor();
    }

    // Update is called once per frame
    private void Update()
    {
        if ((Time.time - last_change_time) > change_CD_seconds)
        {
            RandomColor();
        }
    }
}
