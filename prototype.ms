fn add(lhs, rhs) lhs + rhs

fn sum(a, b, c, d) {
    return add(add(a, b), add(c, d))
}

struct Vec2 (
    x: f32,
    y: f32,
)

var v = Vec2(8, 16)
var w = Vec2(7, 21)

print("  ")
println(add(v.x, v.y))
print("+ ")
println(add(w.x, x.y))
println(sum(v.x, v.y, w.x, w.y))
