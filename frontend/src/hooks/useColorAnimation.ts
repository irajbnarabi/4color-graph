import { useEffect, useState } from "react";

export function useColorAnimation(colors: number[], delay = 400): number[] {
    const [animatedColors, setAnimatedColors] = useState<number[]>([]);

    useEffect(() => {
        if (!colors?.length) return;

        const temp = new Array(colors.length).fill(-1);
        setAnimatedColors([...temp]);

        let i = 0;
        const interval = setInterval(() => {
            temp[i] = colors[i];
            setAnimatedColors([...temp]);
            i++;
            if (i >= colors.length) clearInterval(interval);
        }, delay);

        return () => clearInterval(interval);
    }, [colors]);

    return animatedColors;
}
